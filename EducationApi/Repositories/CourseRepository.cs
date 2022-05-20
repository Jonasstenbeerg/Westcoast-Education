using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
using EducationApi.ViewModels.Course;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repository
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
            return await _context.Courses.Include(c => c.Category)
            .Include(c => c.Teacher)
            .Where(c => c.Category.CategoryName!.ToLower().Contains(category.ToLower()))
            .ProjectTo<CourseViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<CourseViewModel> GetCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if(course is null)
            {
                throw new Exception($"Det finns ingen kurs med nummer {id}");
            }

            var courseToReturn = _mapper.Map<CourseViewModel>(course);

            return courseToReturn;
        }

        public async Task<List<CourseViewModel>> ListAllCoursesAsync()
        {
            return await _context.Courses
            .ProjectTo<CourseViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateCourseAsync(int id, PostCourseViewModel model)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);

            if(courseToUpdate is null)
            {
                throw new Exception($"Det finns inge kurs med nummer {id}");
            }

            var course = _mapper.Map<PostCourseViewModel, Course>(model, courseToUpdate);

            _context.Courses.Update(course);
        }

        public async Task UpdateCourseAsync(int id, PatchCourseViewModel model)
        {
            var courseToUpdate = await _context.Courses.FindAsync(id);

            if(courseToUpdate is null)
            {
                throw new Exception($"Det finns inge kurs med nummer {id}");
            }

            courseToUpdate.LenghtInHouers = model.LenghtInHouers;
            courseToUpdate.Description = model.Description;

            _context.Courses.Update(courseToUpdate);
        }
    }
}