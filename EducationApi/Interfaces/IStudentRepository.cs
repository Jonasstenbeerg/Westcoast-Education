using EducationApi.ViewModels.Student;

namespace EducationApi.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModel>> ListAllStudentsAsync();
        public Task<StudentViewModel> GetStudentByIdAsync(int id);
        public Task AddStudentAsync(PostStudentViewModel model);
        public Task DeleteStudentAsync(int id);
        public Task UpdateStudentAsync(int id, PostStudentViewModel model);
        public Task UpdateStudentAsync(int id, PatchStudentViewModel model);
        public Task<bool> SaveAllAsync();
    }
}