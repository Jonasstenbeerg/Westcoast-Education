using AdministratorClientMVC.Models;
using AdministratorClientMVC.ViewModels;
using AdministratorClientMVC.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace AdministratorClientMVC.Controllers
{
    [Route("[controller]")]
    public class TeacherController : Controller
    {
        private readonly ILogger<TeacherController> _logger;
        private readonly TeacherServiceModel _serviceModel;

        public TeacherController(ILogger<TeacherController> logger,TeacherServiceModel serviceModel)
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

        [HttpGet("TeacherCompetencies")]
        public  async Task<IActionResult> TeacherCompetencies(int id)
        {
            try
            {
                ViewBag.TeacherId=id;
                
                var competencies = await _serviceModel.PointAtTeacherCompAsync(id);
                
                return View("TeacherCompetencies", competencies);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("TeacherCompetencies")]
        public  async Task<IActionResult> TeacherCompetencies(int id,List<AsignCompetenceViewModel> model)
        {
            try
            {
                ViewBag.TeacherId=id;
                
                await _serviceModel.EditCompetenciesForTeacherAsync(id,model);

                var teacher = await _serviceModel.GetTeacherToUpdateByIdAsync(id);
                
                return View("Update", teacher);
            }
            catch (System.Exception ex)
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpGet("AddCompetence")]
        public IActionResult AddCompetence(int id)
        {
            try
            {
                ViewBag.TeacherId = id;
                return View("AddCompetence");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("AddCompetence")]
        public async Task<IActionResult> AddCompetence(int id,CreateCompetenceViewModel model)
        {
            try
            {
                ViewBag.TeacherId = id;

                if (ModelState.IsValid)
                {
                    await _serviceModel.AddTeacherCompetenceAsync(id,model);
                    
                    var competencies = await _serviceModel.PointAtTeacherCompAsync(id);
                
                    return View("TeacherCompetencies", competencies);
                }
                
                return View("AddCompetence",model);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }
        
        [HttpGet("AddTeacher")]
        public  IActionResult AddTeacher()
        {
            try
            {
                return View("AddTeacher");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(CreateTeacherViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _serviceModel.AddTeacherAsync(model);
                    
                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    
                    return View("Index",indexPage);
                }

                return View("AddTeacher",model);
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
                var teacher = await _serviceModel.GetTeacherByIdAsync(id);
                return View("Details",teacher);
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
                ViewBag.TeacherId = id;
                var teacher = await _serviceModel.GetTeacherToUpdateByIdAsync(id);
                return View("Update",teacher);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return View("Error");
            }
            
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(int id,CreateTeacherViewModel model)
        {
            try
            {
                 ViewBag.TeacherId = id;

                if (await _serviceModel.NochangesMadeAsync(id,model))
                {
                    var indexPage = await _serviceModel.LoadIndexPageAsync();
                    return View("Index",indexPage);
                }
                
               if (ModelState.IsValid)
               {
                    await _serviceModel.UpdateTeacherAsync(id,model);
                
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
                await _serviceModel.DeleteTeacherAsync(id);

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