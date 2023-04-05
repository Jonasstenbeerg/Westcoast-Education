using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class StudentCourses
    {
        //Composite key's set in Data/EducationContext
        
        public int StudentId { get; set; }
        
        public int CourseId { get; set; }
        //Navprop
       [ForeignKey("StudentId")]
       public Student Student { get; set; } = new Student();
       [ForeignKey("CourseId")]
        public Course Course { get; set; } = new Course();

    }
}