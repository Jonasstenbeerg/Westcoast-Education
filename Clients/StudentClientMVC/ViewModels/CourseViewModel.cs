using System.ComponentModel.DataAnnotations;

namespace StudentClientMVC.ViewModels
{
    public class CourseViewModel
    {
        [Display(Name ="Kursummer")]
       public int CourseNumber { get; set; }
        [Display(Name ="Kurstitel")]
        public string? CourseTitle { get; set; }
        [Display(Name ="Längd i timmar")]
        public int LenghtInHouers { get; set; }
        [Display(Name ="Beskrivning")]
        public string? Description { get; set; }
        [Display(Name ="Kursmoment")]
        public string? CourseDetails { get; set; }
        [Display(Name ="Kategori")]
        public string? CategoryName { get; set; }
        [Display(Name ="Lärare")]
        public string? TeacherFullName { get; set; }
    }
}