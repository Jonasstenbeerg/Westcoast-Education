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
        public async Task<ActionResult<List<CourseViewModelLessInfo>>> ListAllCoursesAsync()
        {
            try
            {
                var respons = await _courseRepo.ListAllCoursesAsync();

                return Ok(respons);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
           
        }
        [HttpGet("bycategory/{courseCategory}")]
        public async Task<ActionResult<CourseViewModelLessInfo>> GetCourseByCategory(string courseCategory)
        {
             try
            {
                var response =  await _courseRepo.GetCourseByCategoryAsync(courseCategory);

                return Ok(response);
            }
            catch (System.Exception ex)
            {
               
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("bystudentid/{id}")]
        public async Task<ActionResult<List<CourseViewModelLessInfo>>> GetCoursesByStudentId(int id)
        {
             try
            {
                var courses =  await _courseRepo.GetCourseByStudentIdAsync(id);

                return Ok(courses);
            }
            catch (System.Exception ex)
            {
               
                return StatusCode(500,ex.Message);
            }
        }
         
        [HttpGet("byid/{id}")]
        public async Task<ActionResult<CourseViewModel>> GetCourseById(int id)
        {
            try
            {
                var respons = await _courseRepo.GetCourseAsync(id);
                
                return Ok(respons);    
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> AddCourse(PostCourseViewModel model)
        {
            try
            {
               await _courseRepo.AddCourseAsync(model);
               
               if (await _courseRepo.SaveAllAsync())
               {
                   return StatusCode(201);
               }

               return StatusCode(500,"Något gick fel vid skapande av kurs");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id,PostCourseViewModel model)
        {
            try
            {
                await _courseRepo.UpdateCourseAsync(id,model);

                if (await _courseRepo.SaveAllAsync())
               {
                   return NoContent();
               }

               return StatusCode(500,"Något gick fel vid updatering av kurs");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseRepo.DeleteCourseAsync(id);

                if (await _courseRepo.SaveAllAsync())
                {
                    return NoContent();
                }
                
                return StatusCode(500,"Något gick fel vid borttagande av kurs");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);                
            }
        }
    }
}