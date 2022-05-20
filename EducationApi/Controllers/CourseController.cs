using EducationApi.Helpers;
using EducationApi.Interfaces;
using EducationApi.ViewModels.Course;
using Microsoft.AspNetCore.Mvc;

namespace EducationApi.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
       
        private readonly ICourseRepository _courseRepo;

        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
            
        }
        [HttpGet("list")]
        public async Task<ActionResult<List<CourseViewModel>>> ListAllCoursesAsync()
        {
            var respons = await _courseRepo.ListAllCoursesAsync();
           
           if(!respons.Any())
           {
              return NotFound("Din sökning gav inget resultat");
           }
           
           return Ok(respons);
        }
        [HttpGet("{courseCategory}")]
        public async Task<ActionResult<CourseViewModelLessInfo>> GetCourseByCategory(string courseCategory)
        {
            var response =  await _courseRepo.GetCourseByCategoryAsync(courseCategory);

            if(response is null)
            {
                return NotFound($"Det finns ingen kategori som heter {courseCategory}");
            }

            return Ok(response);
        }
    }
}