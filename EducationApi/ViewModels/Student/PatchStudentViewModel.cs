using System.ComponentModel.DataAnnotations;
using EducationApi.Models;

namespace EducationApi.ViewModels.Student
{
    public class PatchStudentViewModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public Address Adress { get; set; } = new Address();
    }
}