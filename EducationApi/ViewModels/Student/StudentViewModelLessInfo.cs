using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApi.ViewModels.Student
{
    public class StudentViewModelLessInfo
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}