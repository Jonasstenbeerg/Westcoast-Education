using System.ComponentModel.DataAnnotations;

namespace EducationApi.ViewModels.Course
{
    public class PostCourseViewModel
    {
        [Required]
        public int CourseNumber { get; set; }
        [Required]
        public string? CourseTitle { get; set; }
        [Required]
        public int LenghtInHouers { get; set; }
        [Required]        
        public string? Description { get; set; }
        [Required]        
        public string? CategoryName { get; set; }
        [Required]
        public string? TeacherFullName { get; set; }
    }
}