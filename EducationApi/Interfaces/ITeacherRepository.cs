using EducationApi.ViewModels.Teacher;

namespace EducationApi.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModel>> ListAlTeachersAsync();
        public Task<TeacherViewModel> GetTeacherAsync(int id);
        public Task AddTeacherAsync(PostTeacherViewModel model);
        public Task DeleteTeacherAsync(int id);
        public Task UpdateTeacherAsync(int id, PostTeacherViewModel model);
        public Task UpdateTeacherAsync(int id, PatchTeacherViewModel model);
        public Task<bool> SaveAllAsync();
    }
}