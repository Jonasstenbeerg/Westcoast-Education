using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApi.Models
{
    public class CourseDetail
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Namn på kurs modul är obligatoriskt")]
        public string? Module { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; } = new Course();
        
    }
}