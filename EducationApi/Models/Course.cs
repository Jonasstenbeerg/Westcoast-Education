using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class Course
    {
        [Key]
        public int CourseNumber { get; set; }
        [Required(ErrorMessage ="Kurstitel är obligatoriskt")]
        public string? CourseTitle { get; set; }
        [Required(ErrorMessage ="Kurslängd är obligatoriskt")]
        public int LenghtInHouers { get; set; }
        [Required(ErrorMessage ="Beskrivning av kurs är obligatoriskt")]
        public string? Description { get; set; }
        [Required(ErrorMessage ="Detaljer om kursen är oligatoriskt")]
        public string? CourseDetails { get; set; }
        //Foreign key's
        [Required]
        public int CategoryId { get; set; }
        public int? TeacherId { get; set; }
        //Navprop
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = new Category();
        [ForeignKey("TeacherId")]
        public Teacher? Teacher { get; set; }
        public ICollection<StudentCourses> Students { get; set; } = new List<StudentCourses>();

        
    }
}