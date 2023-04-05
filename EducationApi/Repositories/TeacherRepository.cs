using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
using EducationApi.ViewModels.Competence;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;

namespace EducationApi.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly EducationContext _context;
        private readonly IMapper _mapper;
        public TeacherRepository(EducationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }

        public async Task AddCompToTeacherAsync(int id, PostCompetenceViewModel model)
        {
            var teacher = await  _context.Teachers.FindAsync(id);

            if (teacher is null)
            {
                throw new Exception($"Det finns ingen lärare med id {id}");
            }

            var compToAdd = _mapper.Map<PostCompetenceViewModel, Competence>(model);
            
            //Kontrolerar att kompetensen inte redan finns i db
            var respons = await SearchCompByNameAsync(model.CompetenceName!);
            
            if(respons != null)
            {
                //Om det finns en kompetens med samma namn så länkas den med läraren
                compToAdd = respons;
                
            } 
           
            await _context.TeacherCompetencies.AddAsync(new TeacherCompetencies
            {
                Competence = compToAdd,
                Teacher = teacher
            });     

        }

        public async Task RemoveCompFromTeacherAsync(int id, PostCompetenceViewModel model)
        {
            var teacher = await  _context.Teachers.FindAsync(id);

            if (teacher is null)
            {
                throw new Exception($"Det finns ingen lärare med id {id}");
            }

           var Tc = await _context.TeacherCompetencies
           .Include(tc =>tc.Competence)
           .Where(tc=>tc.Competence.Name==model.CompetenceName&&tc.Teacher==teacher)
           .FirstOrDefaultAsync();

            _context.TeacherCompetencies.Remove(Tc!);
        }

        public async Task AddTeacherAsync(PostTeacherViewModel model)
        {
            var teacher = _mapper.Map<PostTeacherViewModel, Teacher>(model);

            //Kontrolerar om adressen redan finns i databasen
            var address = await SearchAddressAsync(teacher.UserInfo.Address);

            if(address != null)
            {
               teacher.UserInfo.Address = address;         
            }

            await _context.Teachers.AddAsync(teacher);
        }

        public async Task AddTeacherToCourseAsync(int id, CourseSearchModel model)
        {
           var course = await _context.Courses
           .FirstOrDefaultAsync(course => course.CourseNumber==model.CourseNumber);

           var teacher = await _context.Teachers.FindAsync(id);

           if (course is null || teacher is null)
           {
               throw new Exception("Läraren eller kursen hittades inte");
           }

           course.Teacher = teacher;

            _context.Courses.Update(course);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacherToDelete = await _context.UserInfos
            .Include(ui=>ui.Address)
            .FirstOrDefaultAsync(ui=>ui.Id==id);

            if(teacherToDelete is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            _context.UserInfos.Remove(teacherToDelete);

        }

        public async Task<TeacherViewModel> GetTeacherAsync(int id)
        {
            var teacher = await _context.Teachers
            .Include(teacher=>teacher.UserInfo)
            .ThenInclude(ui=>ui.Address)
            .FirstOrDefaultAsync(teacher=>teacher.Id==id);

            if(teacher is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            var teacherToReturn = _mapper.Map<Teacher, TeacherViewModel>(teacher);

            return teacherToReturn;
        }

        public async Task<TeacherViewModel> GetTeacherByCourseAsync(int id)
        {
            var teacher = await _context.Courses
            .Include(c=>c.Teacher)
            .Where(c=>c.CourseNumber==id)
            .Select(c=>c.Teacher)
            .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
            
            if (teacher is null)
            {
                throw new Exception($"Ingen lärare till kursen med id {id} kunde hittas");
            }

            return teacher;
        }

        public async Task<List<TeacherViewModelLessInfo>> ListAlTeachersAsync()
        {
            return await _context.Teachers
            .ProjectTo<TeacherViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<List<TeacherViewModelLessInfo>> ListTeachersByCompAsync(string competence)
        {
            return await _context.TeacherCompetencies
            .Include(tc => tc.Competence)
            .Include(tc => tc.Teacher)
            .Where(tc => tc.Competence.Name!.ToLower()==competence.ToLower())
            .Select(tc => tc.Teacher)
            .ProjectTo<TeacherViewModelLessInfo>(_mapper.ConfigurationProvider)
            .ToListAsync();
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

        public async Task UpdateTeacherAsync(int id, PostTeacherViewModel model)
        {
            var teacherToUpdate = await _context.Teachers
            .Include(teacher=>teacher.UserInfo)
            .ThenInclude(ui=>ui.Address)
            .FirstOrDefaultAsync(teacher=>teacher.Id==id);

            if(teacherToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            var teacher = _mapper.Map<PostTeacherViewModel, Teacher>(model,teacherToUpdate);

            //Kontrolerar om adressen redan finns i databasen
            var address = await SearchAddressAsync(teacher.UserInfo.Address);

            if(address != null)
            {
               teacher.UserInfo.AddressId = address.Id;         
            }

            _context.Teachers.Update(teacher);
        }

         private async Task<Address?> SearchAddressAsync(Address address)
        {
            return await _context.Addresses
            .Where(a => a.StreetName!.ToLower()==address.StreetName!.ToLower()&&a.StreetNumber==address.StreetNumber&&a.Zipcode==address.Zipcode)
            .FirstOrDefaultAsync();
        }
        
        private async Task<Competence?> SearchCompByNameAsync(string name)
        {
            return await _context.Competencies
            .Where(a => a.Name!.ToLower()==name!.ToLower())
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