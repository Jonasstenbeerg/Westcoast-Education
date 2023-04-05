using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApi.ViewModels.Teacher
{
    public class TeacherViewModelLessInfo
    {
        public int TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}