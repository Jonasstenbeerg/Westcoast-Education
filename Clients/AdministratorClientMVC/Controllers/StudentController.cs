using AdministratorClientMVC.Models;
using AdministratorClientMVC.ViewModels.Course;
using AdministratorClientMVC.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace AdministratorClientMVC.Controllers
{
    [Route("[controller]")]
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly StudentServiceModel _serviceModel;

        public StudentController(ILogger<StudentController> logger,StudentServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
            _logger = logger;
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var indexPage = await _serviceModel.LoadIndexPageAsync();
                return View("Index",indexPage);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("Index")]
        public async Task<IActionResult> Index(string input)
        {
            
            try
            {
                var newPage = await _serviceModel.UpdateIndexPageAsync(input);
                return View("Index",newPage);
            }
            catch (System.Exception ex) 
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
        }

        [HttpGet("AddStudent")]
        public  IActionResult AddStudent()
        {
            try
            {
                return View("AddStudent");
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent(CreateStudentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceModel.AddStudentAsync(model);
                    
                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    
                    return View("Index",indexPage);
                }

                return View("AddStudent",model);
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpGet("StudentCourses")]
        public async Task<IActionResult> StudentCourses(int id)
        {
            try
            {
                ViewBag.StudentId=id;

                var courses = await _serviceModel.PointAtStudentCoursesAsync(id);

                return View("StudentCourses",courses);
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("StudentCourses")]
        public  async Task<IActionResult> StudentCourses(int id,List<AsignCourseViewModel> model)
        {
            try
            {
                ViewBag.StudentId = id;

                await _serviceModel.EditCoursesForStudentAsync(id,model);

                var student = await _serviceModel.GetStudentToUpdateByIdAsync(id);
                
                return View("Update",student);
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var student = await _serviceModel.GetStudentByIdAsync(id);
                return View("Details",student);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpGet("Update")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                ViewBag.StudentId = id;
                var student = await _serviceModel.GetStudentToUpdateByIdAsync(id);
                return View("Update",student);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int id,CreateStudentViewModel model)
        {
            try
            {
                ViewBag.StudentId = id;

                if (await _serviceModel.NochangesMadeAsync(id,model))
                {
                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    return View("Index",indexPage);
                }
                
                if (ModelState.IsValid)
                {
                    await _serviceModel.UpdateStudentAsync(id,model);

                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    return View("Index",indexPage);
                }
                
                return View("Update",model);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceModel.DeleteStudentAsync(id);

                var page = await _serviceModel.LoadIndexPageAsync();

                return View("Index",page);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }
    }
}