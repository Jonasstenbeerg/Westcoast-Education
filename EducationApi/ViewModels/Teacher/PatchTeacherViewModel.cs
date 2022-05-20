using System.ComponentModel.DataAnnotations;
using EducationApi.Models;

namespace EducationApi.ViewModels.Teacher
{
    public class PatchTeacherViewModel
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public Address Address { get; set; } = new Address();
        [Required]
        public List<Competence> TeacherCompetencies { get; set; } = new List<Competence>();
    
    }
}