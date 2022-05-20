using System.ComponentModel.DataAnnotations;

namespace EducationApi.ViewModels.Student
{
    public class PostStudentViewModel
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Adress { get; set; }
    }
}