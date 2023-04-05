using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels.Course
{
    public class CourseViewModel
    {
        [Display(Name = "Kursnummer")]
        public int CourseNumber { get; set; }
        [Display(Name = "Kurstitel")]
        public string? CourseTitle { get; set; }
        [Display(Name = "Längd i timmar")]
        public int? LenghtInHouers { get; set; }
        [Display(Name = "Beskrivning")]
        public string? Description { get; set; }
        [Display(Name = "Kursmoment")]
        public string? CourseDetails { get; set; }
        [Display(Name = "Kategori")]
        public string? CategoryName { get; set; }
        [Display(Name = "Lärare")]
        public string? TeacherFullName { get; set; }
        
    }
}
