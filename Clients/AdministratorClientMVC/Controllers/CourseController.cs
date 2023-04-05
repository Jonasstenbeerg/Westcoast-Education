using AdministratorClientMVC.Models;
using AdministratorClientMVC.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace AdministratorClientMVC.Controllers
{
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseServiceModel _serviceModel;
        private readonly TeacherServiceModel _teacherServiceModel;

        public CourseController(ILogger<CourseController> logger,CourseServiceModel serviceModel,TeacherServiceModel teacherServiceModel)
        {
            _teacherServiceModel = teacherServiceModel;
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

        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var course = await _serviceModel.GetCourseByIdAsync(id);
                return View("Details",course);
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
                ViewBag.CourseId = id;
                var course = await _serviceModel.GetCourseToUpdateByIdAsync(id);
                return View("Update",course);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(int id,CreateCourseViewModel model)
        {
            try
            {
                ViewBag.CourseId = id;
               
               if (ModelState.IsValid)
               {
                    await _serviceModel.UpdateCourseAsync(id,model);
                
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

        [HttpGet("AddCourse")]
        public  IActionResult AddCourse()
        {
            try
            {
                return View("AddCourse");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(CreateCourseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceModel.AddCourseAsync(model);
                    
                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    
                    return View("Index",indexPage);
                }

                return View("AddCourse",model);
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
                await _serviceModel.DeleteCourseAsync(id);

                var page = await _serviceModel.LoadIndexPageAsync();

                return View("Index",page);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpGet("CourseTeacher")]
        public async Task<IActionResult> CourseTeacher(int courseId)
        {
            try
            {
                ViewBag.CourseId = courseId;

                var teachers = await _teacherServiceModel.PointAtTheacherInListAsync(courseId);

                return View("CourseTeacher",teachers);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("CourseTeacher")]
        public async Task<IActionResult> CourseTeacher(int teacherId,int courseId)
        {
           
            try
            {
                ViewBag.CourseId = courseId;

                await _teacherServiceModel.SetCourseTeacherAsync(teacherId,courseId);

                var course = await _serviceModel.GetCourseToUpdateByIdAsync(courseId);

                return View("Update",course);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
           
        }


    }
}