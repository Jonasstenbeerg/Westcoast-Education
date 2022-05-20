using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
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
        public async Task AddTeacherAsync(PostTeacherViewModel model)
        {
            var teacher = _mapper.Map<PostTeacherViewModel, Teacher>(model);

            model.TeacherCompetencies
            .ForEach(async competence =>await _context.TeacherCompetencies
            .AddAsync(new TeacherCompetencies{Teacher = teacher,Competence = competence}));

            await _context.Teachers.AddAsync(teacher);
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacherToDelete = await _context.Teachers.FindAsync(id);

            if(teacherToDelete is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            _context.Teachers.Remove(teacherToDelete);
        }

        public async Task<TeacherViewModel> GetTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);

            if(teacher is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            var teacherToReturn = _mapper.Map<Teacher, TeacherViewModel>(teacher);

            return teacherToReturn;
        }

        public async Task<List<TeacherViewModel>> ListAlTeachersAsync()
        {
            return await _context.Teachers
            .ProjectTo<TeacherViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateTeacherAsync(int id, PostTeacherViewModel model)
        {
            var teacherToUpdate = await _context.Teachers.FindAsync(id);

            if(teacherToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            var teacher = _mapper.Map<PostTeacherViewModel, Teacher>(model);

            model.TeacherCompetencies
            .ForEach(async competence =>await _context.TeacherCompetencies
            .AddAsync(new TeacherCompetencies{Teacher = teacherToUpdate,Competence = competence}));

            _context.Teachers.Update(teacher);
        }

        public async Task UpdateTeacherAsync(int id, PatchTeacherViewModel model)
        {
            var teacherToUpdate = await _context.Teachers.Include(teacher => teacher.UserInfo).FirstOrDefaultAsync(teacher => teacher.Id==id);

            if(teacherToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon lärare med id {id}");
            }

            teacherToUpdate.UserInfo.Email = model.Email;
            teacherToUpdate.UserInfo.PhoneNumber = model.PhoneNumber;
            teacherToUpdate.UserInfo.Address = model.Address;

            model.TeacherCompetencies
            .ForEach(async competence =>await _context.TeacherCompetencies
            .AddAsync(new TeacherCompetencies{Teacher = teacherToUpdate,Competence = competence}));

            _context.Teachers.Update(teacherToUpdate);
        }
    }
}