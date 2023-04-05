using EducationApi.Interfaces;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace EducationApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        
        public StudentController(IStudentRepository studentRepo)
        {
            
            _studentRepo = studentRepo;
            
        }
        [HttpGet("list")]
        public async Task<ActionResult<List<StudentViewModelLessInfo>>> ListAllStudent()
        {
            return Ok(await _studentRepo.ListAllStudentsAsync());
        }
        [HttpGet("byid/{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentById(int id)
        {
            try
            {
               var respons = await _studentRepo.GetStudentByIdAsync(id);

               return Ok(respons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }
        [HttpGet("bycoursenumber/{courseNumber}")]
        public async Task<ActionResult<List<StudentViewModelLessInfo>>> GetStudentByCourseNumber(int courseNumber)
        {
            try
            {
               var respons = await _studentRepo.ListAllStudentsByCourseNumberAsync(courseNumber);

               return Ok(respons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }
        [HttpPost("addcourse/{studentId}")]
        public async Task<ActionResult> AddStudentToCourse(int studentId, CourseSearchModel model)
        {
            try
            {
                await _studentRepo.AddStudentToCourseAsync(studentId,model);

                if(await _studentRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel vid registrering av student till kurs");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);  
            }
        }
        [HttpPost("removecourse/{studentId}")]
        public async Task<ActionResult> RemoveStudentFromCourse(int studentId, CourseSearchModel model)
        {
            try
            {
                await _studentRepo.RemoveStudentFromCourseAsync(studentId,model);

                if(await _studentRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel vid borttagande av student från kurs");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);  
            }
        }
        [HttpPost("signupjoin/{courseId}")]
        public async Task<ActionResult> AddStudentToCourse(int courseId, PostStudentViewModel model)
        {
            try
            {
              await _studentRepo.AddStudentToCourseAsync(courseId,model);

              if (await _studentRepo.SaveAllAsync())
              {
                  return StatusCode(201);
              }
              return StatusCode(500,"Något gick fel vid registrering av elev till kurs");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> AddStudent(PostStudentViewModel model)
        {
            try
            {
                await _studentRepo.AddStudentAsync(model);

                if(await _studentRepo.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500,"Ett fel inträffade vid registrering av student");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudent(int id, PostStudentViewModel model)
        {
            try
            {
                await _studentRepo.UpdateStudentAsync(id,model);

                if(await _studentRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel vid uppdatering av student");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }
       
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            try
            {
                await _studentRepo.DeleteStudentAsync(id);

                if(await _studentRepo.SaveAllAsync())
                {
                    return NoContent();
                }
                return StatusCode(500,"Något gick fel vid borttagande av student");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);                
            }
        }

    }
}