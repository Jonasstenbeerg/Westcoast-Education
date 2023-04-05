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
        public string? StreetName { get; set; }
        [Required]
        public int StreetNumber { get; set; }
        [Required]
        public int Zipcode { get; set; }
        
    }
}