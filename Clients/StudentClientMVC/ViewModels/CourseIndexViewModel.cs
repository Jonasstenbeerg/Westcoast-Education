using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentClientMVC.ViewModels
{
    public class CourseIndexViewModel
    {
        public List<CourseViewModelLessInfo> Courses { get; set; } = new List<CourseViewModelLessInfo>();

        public List<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}