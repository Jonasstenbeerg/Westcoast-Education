using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministratorClientMVC.ViewModels.Course
{
    public class AsignCourseViewModel
    {
        public int CourseNumber { get; set; }
        public string? CourseTitle { get; set; }
        public bool HasCourse { get; set; }
    }
}