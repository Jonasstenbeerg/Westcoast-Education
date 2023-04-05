using Microsoft.AspNetCore.Mvc;
using StudentClientMVC.Models;
using StudentClientMVC.ViewModels;

namespace StudentClientMVC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentServiceModel _serviceModel;

        public StudentController(ILogger<StudentController> logger ,StudentServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> Signup(CourseSignupViewModel model,int courseId)
        {
          try
          {
            
            ViewBag.CourseId = courseId;

            if (!ModelState.IsValid)
            {
                return View("Signup",model);
            }

            if (await _serviceModel.SignupStudentAsync(model,courseId))
            {
                return View("Confirmation");
            }
            
            return View("Signup",model);
          }
          catch (System.Exception ex)
          {
             _logger.LogError(ex.Message);
            return View("Error");
          }
        }


        [HttpGet]
        public IActionResult Signup(int courseId)
        {
          try
          {
            ViewBag.CourseId = courseId;
            return View("Signup");
          }
          catch (System.Exception ex)
          {
             _logger.LogError(ex.Message);
            return View("Error");
          }
            
        }

        
    }
}