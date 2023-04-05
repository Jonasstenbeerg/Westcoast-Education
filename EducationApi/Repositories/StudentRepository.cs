using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Student;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EducationContext _context;
        private readonly IMapper _mapper;
        public StudentRepository(EducationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }
        public async Task AddStudentAsync(PostStudentViewModel model)
        {
            var studentToAdd = _mapper.Map<Student>(model);

            //Kontrolerar om adressen redan finns i databasen
            var address = await SearchAddressAsync(studentToAdd.UserInfo.Address);

            if(address != null)
            {
               studentToAdd.UserInfo.Address = address;         
            }

            await _context.Students.AddAsync(studentToAdd);
        }


        public async Task AddStudentToCourseAsync(int id, CourseSearchModel model)
        {
           var course = await _context.Courses
           .FirstOrDefaultAsync(course => course.CourseNumber==model.CourseNumber);

           var student = await _context.Students.FindAsync(id);

           if (course is null || student is null)
           {
               throw new Exception("Studenten eller kursen hittades inte");
           }

          await _context.StudentCourses.AddAsync( new StudentCourses{
              Course = course!,
              Student = student!  
          });
        }

        public async Task AddStudentToCourseAsync(int id, PostStudentViewModel model)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course is null)
            {
                throw new Exception($"Det finns inge kurs med id {id}");
            }

            var studentToAdd = _mapper.Map<Student>(model);

            var address = await SearchAddressAsync(studentToAdd.UserInfo.Address);

            if(address != null)
            {
               studentToAdd.UserInfo.Address = address;         
            }

            _context.StudentCourses.Add(new StudentCourses{Course=course,Student=studentToAdd});
        }

         public async Task RemoveStudentFromCourseAsync(int id, CourseSearchModel model)
        {
           var course = await _context.Courses
           .FirstOrDefaultAsync(course => course.CourseNumber==model.CourseNumber);

           var student = await _context.Students.FindAsync(id);

           if (course is null || student is null)
           {
               throw new Exception("Studenten eller kursen hittades inte");
           }

            var studentCourse = await _context.StudentCourses
            .Include(sc=>sc.Course).Include(sc=>sc.Student)
            .Where(sc=>sc.Course==course&&sc.Student==student)
            .FirstOrDefaultAsync();

            if (studentCourse is null)
            {
                throw new Exception("Studenten och kursen har ingen koppling");
            }

            _context.StudentCourses.Remove(studentCourse);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var studentToRemove = await _context.UserInfos
            .Include(ui=>ui.Address)
            .FirstOrDefaultAsync(ui=>ui.Id==id);

            if(studentToRemove is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            _context.UserInfos.Remove(studentToRemove);

        }

        public async Task<StudentViewModel> GetStudentByIdAsync(int id)
        {
           var student = await _context.Students
           .Include(student=>student.UserInfo)
           .ThenInclude(ui=>ui.Address)
           .FirstOrDefaultAsync(student=>student.Id==id);

            if(student is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            var studentToReturn = _mapper.Map<StudentViewModel>(student);

            return studentToReturn;
        }

        public Task<List<StudentViewModelLessInfo>> ListAllStudentsAsync()
        {
            return _context.Students.Include(student=>student.UserInfo)
            .ProjectTo<StudentViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<List<StudentViewModelLessInfo>> ListAllStudentsByCourseNumberAsync(int courseNumber)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(course=>course.CourseNumber==courseNumber);

            if(course is null)
            {
                throw new Exception($"Det finns ingen kurs med kursnummer {courseNumber}");
            }
            
            var students = await _context.StudentCourses
            .Include(sc=>sc.Course)
            .Include(sc=>sc.Student)
            .ThenInclude(student=>student.UserInfo)
            .ThenInclude(ui=>ui.Address)
            .Where(sc => sc.CourseId==courseNumber)
            .Select(sc => sc.Student)
            .ProjectTo<StudentViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
            if (students is null)
            {
                throw new Exception("Denna kurs har inga studenter");
            }
            
            return students;
        }

        public async Task<bool> SaveAllAsync()
        {
            if(await _context.SaveChangesAsync() > 0)
            {
                await RemoveEmptyAddressAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateStudentAsync(int id, PostStudentViewModel model)
        {
            var studentToUpdate = await _context.Students
            .Include(student=>student.UserInfo)
            .ThenInclude(ui=>ui.Address)
            .FirstOrDefaultAsync(student=>student.Id==id);
            
            if(studentToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            var student = _mapper.Map<PostStudentViewModel, Student>(model,studentToUpdate);

            //Kontrolerar om adressen redan finns i databasen
            var address = await SearchAddressAsync(student.UserInfo.Address);

            if(address != null)
            {
               student.UserInfo.AddressId = address.Id;
            }

            _context.Students.Update(student);

           
        }

         private async Task<Address?> SearchAddressAsync(Address address)
        {
            return await _context.Addresses
            .Where(a => a.StreetName!.ToLower()==address.StreetName!.ToLower()&&a.StreetNumber==address.StreetNumber&&a.Zipcode==address.Zipcode)
            .FirstOrDefaultAsync();
        }

        private async Task RemoveEmptyAddressAsync()
        {
            var Addresses = await _context.Addresses
           .Include(a=>a.Users)
           .ToListAsync();

            foreach (Address _address in Addresses)
            {
                if (_address.Users.Count()==0)
                {
                    _context.Addresses.Remove(_address);
                }
            }
        }

       
    }
}