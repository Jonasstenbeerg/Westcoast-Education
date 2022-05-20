using System.ComponentModel.DataAnnotations;

namespace EducationApi.ViewModels.Course
{
    public class PatchCourseViewModel
    {
        [Required]
        public int LenghtInHouers { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}