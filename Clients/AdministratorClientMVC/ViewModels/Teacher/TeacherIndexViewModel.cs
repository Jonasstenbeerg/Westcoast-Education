namespace AdministratorClientMVC.ViewModels.Teacher
{
    public class TeacherIndexViewModel
    {
        public List<TeacherViewModelLessInfo> Teachers { get; set; } = new List<TeacherViewModelLessInfo>();
        public List<CompetenceViewModel> Competencies { get; set; } = new List<CompetenceViewModel>();
    }
}