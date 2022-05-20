using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        //Navprop
        [ForeignKey("Id")]
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public ICollection<StudentCourses> Courses { get; set; } = new List<StudentCourses>();
    }
}