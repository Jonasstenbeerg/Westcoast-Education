using AdministratorClientMVC.ViewModels.Course;

namespace AdministratorClientMVC.ViewModels.Student
{
    public class StudentIndexViewModel
    {
        public List<StudentViewModelLessInfo> Students { get; set; } = new List<StudentViewModelLessInfo>();
        public List<CourseViewModelLessInfo> Courses { get; set; } = new List<CourseViewModelLessInfo>();
    }
}