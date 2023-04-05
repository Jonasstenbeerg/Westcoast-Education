using System.ComponentModel.DataAnnotations;

namespace EducationApi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Kategorinamn är obligatoriskt")]
        public string? CategoryName { get; set; }
        //Navprop
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}