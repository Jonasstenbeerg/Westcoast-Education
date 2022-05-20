using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationApi.Models
{
    public class TeacherCompetencies
    {
        //Composite key's set in Data/EducationContext
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int CompetenceId { get; set; }
        //Navprop
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; } = new Teacher();
        [ForeignKey("CompetenceId")]
        public Competence Competence { get; set; } = new Competence();

    }
}