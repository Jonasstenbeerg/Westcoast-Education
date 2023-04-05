using System.ComponentModel.DataAnnotations;

namespace AdministratorClientMVC.ViewModels.Course
{
    public class CreateCourseViewModel
    {
        [Display(Name = "Kurstitel")]
        [Required(ErrorMessage ="Kurstitel är obligatoriskt")]
        public string? CourseTitle { get; set; }
        [Display(Name = "Längd i timmar")]
        [Required(ErrorMessage ="Längd i timmar är obligatoriskt")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Bara siffror kan anges")]
        [Range(1,200, ErrorMessage ="Längd i timmar kan bara vara mellan 1 till 200")]
        public string? LenghtInHouers { get; set; }
        [Display(Name = "Beskrivning")]
        [Required(ErrorMessage ="Beskrivning är obligatoriskt")]        
        public string? Description { get; set; }
        [Display(Name = "Kursmoment")]
        [Required(ErrorMessage ="Kursmoment är obligatoriskt")]
         public string? CourseDetails { get; set; }
        [Display(Name = "Kategori")]
        [Required(ErrorMessage ="Kategori är obligatoriskt")]        
        public string? CategoryName { get; set; }
    }
}