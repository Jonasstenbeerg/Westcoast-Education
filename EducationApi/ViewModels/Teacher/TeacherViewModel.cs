using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApi.Models;

namespace EducationApi.ViewModels.Teacher
{
    public class TeacherViewModel
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
        public List<Competence> TeacherCompetencies { get; set; } = new List<Competence>();
    }
}