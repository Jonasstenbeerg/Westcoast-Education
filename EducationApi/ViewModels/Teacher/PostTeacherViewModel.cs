using System.ComponentModel.DataAnnotations;
using EducationApi.Models;

namespace EducationApi.ViewModels.Teacher
{
    public class PostTeacherViewModel
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
        [Required]
        public List<Competence> TeacherCompetencies { get; set; } = new List<Competence>();
    }
}