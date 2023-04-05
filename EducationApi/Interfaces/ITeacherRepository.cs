using EducationApi.ViewModels.Competence;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Teacher;

namespace EducationApi.Interfaces
{
    public interface ITeacherRepository
    {
        public Task<List<TeacherViewModelLessInfo>> ListAlTeachersAsync();
        public Task<TeacherViewModel> GetTeacherAsync(int id);
        public Task<TeacherViewModel> GetTeacherByCourseAsync(int id);
        public Task<List<TeacherViewModelLessInfo>> ListTeachersByCompAsync(string competence);
        public Task AddTeacherAsync(PostTeacherViewModel model);
        public Task AddCompToTeacherAsync(int id, PostCompetenceViewModel model);
        public Task RemoveCompFromTeacherAsync(int id, PostCompetenceViewModel model);
        public Task AddTeacherToCourseAsync(int id, CourseSearchModel model);
        public Task DeleteTeacherAsync(int id);
        public Task UpdateTeacherAsync(int id, PostTeacherViewModel model);
        public Task<bool> SaveAllAsync();
    }
}