using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Student;

namespace EducationApi.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModelLessInfo>> ListAllStudentsAsync();
        public Task<List<StudentViewModelLessInfo>> ListAllStudentsByCourseNumberAsync(int courseNumber);
        public Task<StudentViewModel> GetStudentByIdAsync(int id);
        public Task AddStudentAsync(PostStudentViewModel model);
        public Task RemoveStudentFromCourseAsync(int id, CourseSearchModel model);
        public Task AddStudentToCourseAsync(int id, CourseSearchModel model);
        public Task AddStudentToCourseAsync(int id, PostStudentViewModel model);
        public Task DeleteStudentAsync(int id);
        public Task UpdateStudentAsync(int id, PostStudentViewModel model);
        public Task<bool> SaveAllAsync();
    }
}