using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApi.ViewModels.Course;

namespace EducationApi.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseViewModelLessInfo>> ListAllCoursesAsync();
        public Task<List<CourseViewModelLessInfo>> GetCourseByCategoryAsync(string category);
        public Task<List<CourseViewModelLessInfo>> GetCourseByStudentIdAsync(int id);
        public Task<CourseViewModel> GetCourseAsync(int id);
        public Task AddCourseAsync(PostCourseViewModel model);
        public Task DeleteCourseAsync(int id);
        public Task UpdateCourseAsync(int id, PostCourseViewModel model);
        public Task<bool> SaveAllAsync();
        
    }
}