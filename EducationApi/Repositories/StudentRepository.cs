using AutoMapper;
using AutoMapper.QueryableExtensions;
using EducationApi.Data;
using EducationApi.Interfaces;
using EducationApi.Models;
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
            var StudentToAdd = _mapper.Map<Student>(model);
            await _context.Students.AddAsync(StudentToAdd);
        }

        public async Task DeleteStudentAsync(int id)
        {
            var studentToRemove = await _context.Students.FindAsync(id);

            if(studentToRemove is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            _context.Students.Remove(studentToRemove);
        }

        public async Task<StudentViewModel> GetStudentByIdAsync(int id)
        {
           var student = await _context.Students.FindAsync(id);

            if(student is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            var studentToReturn = _mapper.Map<StudentViewModel>(student);

            return studentToReturn;
        }

        public Task<List<StudentViewModel>> ListAllStudentsAsync()
        {
            return _context.Students.Include(student=>student.UserInfo)
            .ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateStudentAsync(int id, PostStudentViewModel model)
        {
            var studentToUpdate = await _context.Students.FindAsync(id);

            if(studentToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            var student = _mapper.Map<PostStudentViewModel, Student>(model,studentToUpdate);

            _context.Students.Update(student);
        }

        public async Task UpdateStudentAsync(int id, PatchStudentViewModel model)
        {
            var studentToUpdate = await _context.Students
            .Include(student => student.UserInfo)
            .FirstOrDefaultAsync(student=>student.Id==id);

            if(studentToUpdate is null)
            {
                throw new Exception($"Vi kunde inte hitta någon student med id {id}");
            }

            studentToUpdate.UserInfo.Email = model.Email;
            studentToUpdate.UserInfo.PhoneNumber = model.PhoneNumber;
            studentToUpdate.UserInfo.Address = model.Adress;

            _context.Students.Update(studentToUpdate);
        }
    }
}