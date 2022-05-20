using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        //Navprop
        [ForeignKey("Id")]
        public UserInfo UserInfo { get; set; } = new UserInfo();
        
        public ICollection<TeacherCompetencies> TeacherCompetencies { get; set; } = new List<TeacherCompetencies>();

    }
}