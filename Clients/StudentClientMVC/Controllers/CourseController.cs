using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentClientMVC.Models;

namespace StudentClientMVC.Controllers
{
    [Route("[controller]")]
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private readonly CourseServiceModel _serviceModel;

        public CourseController(ILogger<CourseController> logger,CourseServiceModel serviceModel)
        {
            _serviceModel = serviceModel;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string input)
        {
            
            try
            {
                var page = await _serviceModel.UpdateIndexPageAsync(input);
                return View("Index",page);
            }
            catch (System.Exception ex) 
            {
                 _logger.LogError(ex.Message);
                return View("Error");
            }
        }

        [HttpGet]
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
        
    }
}