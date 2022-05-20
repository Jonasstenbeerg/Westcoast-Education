using System.ComponentModel.DataAnnotations;

namespace EducationApi.Models
{
    public class Competence
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Namn på kompetens är obligatoriskt")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Beskrivning av kompetens är obligatoriskt")]
        public string? Description { get; set; }
        //Navprop
        public ICollection<TeacherCompetencies> TeacherCompetencies { get; set; } = new List<TeacherCompetencies>();
    }
}
