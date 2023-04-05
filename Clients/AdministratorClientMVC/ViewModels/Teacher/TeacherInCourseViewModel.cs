namespace AdministratorClientMVC.ViewModels.Teacher
{
    public class TeacherInCourseViewModel
    {
        public int TeacherId { get; set; }
        public string? TeacherFullName { get; set; }
        public bool IsInCourse { get; set; }
    }
}