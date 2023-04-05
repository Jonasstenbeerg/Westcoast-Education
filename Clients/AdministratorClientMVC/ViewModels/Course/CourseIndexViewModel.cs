namespace AdministratorClientMVC.ViewModels.Course
{
    public class CourseIndexViewModel
    {
        public List<CourseViewModelLessInfo> Courses { get; set; } = new List<CourseViewModelLessInfo>();
        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}