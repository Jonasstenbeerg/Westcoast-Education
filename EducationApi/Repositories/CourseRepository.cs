using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
using EducationApi.ViewModels.Course;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EducationContext _context;
        private readonly IMapper _mapper;
        public CourseRepository(EducationContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task AddCourseAsync(PostCourseViewModel model)
        {
            var courseToAdd = _mapper.Map<Course>(model);

            courseToAdd.CourseNumber = GenerateCoursNumber();

             //Kontrolerar om kategorin redan finns i databasen
            var category = await SearchCategoryAsync(courseToAdd.Category);

            if(category != null)
            {
               courseToAdd.Category = category;         
            }

            await _context.Courses.AddAsync(courseToAdd);
        }

        

        public async Task DeleteCourseAsync(int id)
        {
            var courseToDelete = await _context.Courses.FindAsync(id);

            if(courseToDelete is null)
            {
                throw new Exception($"Någon kurs med nummer {id} finns ej");
            }

            _context.Courses.Remove(courseToDelete);
        }

        public async Task<List<CourseViewModelLessInfo>> GetCourseByCategoryAsync(string category)
        {
            
            var courses = await _context.Courses.Include(c => c.Category)
            .Include(c => c.Teacher)
            .Where(c => c.Category.CategoryName!.ToLower().Contains(category.ToLower()))
            .ProjectTo<CourseViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();

            if(courses.Count() is 0)
            {
                throw new Exception($"Det finns ingen kurs med en kategori som heter något med {category}");
            }

            return courses;
        }

         public async Task<List<CourseViewModelLessInfo>> GetCourseByStudentIdAsync(int id)
        {
            var courses = await _context.StudentCourses
            .Include(sc=>sc.Course)
            .Where(sc=>sc.StudentId==id)
            .Select(sc=>sc.Course)
            .ProjectTo<CourseViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();

            if (courses.Count() < 1)
            {
                throw new Exception("Studenten har inga kurser");
            }

            return courses;
        }   

        public async Task<CourseViewModel> GetCourseAsync(int id)
        {
            var course = await _context.Courses
            .Include(course => course.Category)
            .Include(course => course.Teacher)
            .ThenInclude(teacher => teacher!.UserInfo)
            .FirstOrDefaultAsync(course => course.CourseNumber==id);

            if(course is null)
            {
                throw new Exception($"Det finns ingen kurs med nummer {id}");
            }

            var courseToReturn = _mapper.Map<CourseViewModel>(course);

            return courseToReturn;
        }

        public async Task<List<CourseViewModelLessInfo>> ListAllCoursesAsync()
        {
            return await _context.Courses.Include(c=>c.Category)
            .ProjectTo<CourseViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
            {
                await RemoveEmptyCategoriesAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task RemoveEmptyCategoriesAsync()
        {
            var Categories = await _context.Categories
           .Include(c=>c.Courses)
           .ToListAsync();

            foreach (var category in Categories)
            {
                if (category.Courses.Count()==0)
                {
                    _context.Categories.Remove(category);
                }
            }
        }

        public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
        {
            var courseToUpdate = await _context.Courses
            .Include(course=>course.Category)
            .FirstOrDefaultAsync(course=>course.CourseNumber==id);

            if(courseToUpdate is null)
            {
                throw new Exception($"Det finns inge kurs med nummer {id}");
            }

            if (courseToUpdate.Category.CategoryName!=model.CategoryName)
            {
                courseToUpdate.Category = new Category
                {
                    CategoryName = model.CategoryName
                };
            } 
               
            var course = _mapper.Map<PostCourseViewModel, Course>(model, courseToUpdate);

            _context.Courses.Update(course);
        }

        private async Task<Category?> SearchCategoryAsync(Category category)
        {
            return await _context.Categories
            .Where(c => c.CategoryName!.ToLower()==category.CategoryName!.ToLower())
            .FirstOrDefaultAsync();
        }

        private int GenerateCoursNumber()
        {
            var random = new Random();

            return random.Next(1000,10000);
        }

        
    }
}