using EducationApi.Interfaces;
using EducationApi.ViewModels.Competence;
using EducationApi.ViewModels.Course;
using EducationApi.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;


namespace EducationApi.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;
      
        public TeacherController(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
            
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<TeacherViewModel>>> ListAllTeachers()
        {
            return Ok(await _teacherRepo.ListAlTeachersAsync());
        }
        [HttpGet("bycompetence/{competence}")]
        public async Task<ActionResult<List<TeacherViewModelLessInfo>>> ListTeachersByCompetence(string competence)
        {
            try
            {
               var respons = await _teacherRepo.ListTeachersByCompAsync(competence);

                return Ok(respons);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message); 
            }
        }
        [HttpGet("byid/{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherById(int id)
        {
           try
           {
              var respons = await _teacherRepo.GetTeacherAsync(id);

               return Ok(respons);
           }
           catch (Exception ex)
           {
               return StatusCode(500,ex.Message);
           }     
        }
        [HttpGet("bycourseid/{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherByCourseId(int id)
        {
           try
           {
              var respons = await _teacherRepo.GetTeacherByCourseAsync(id);

               return Ok(respons);
           }
           catch (Exception ex)
           {
               return StatusCode(500,ex.Message);
           }     
        }
        [HttpPost]
        public async Task<ActionResult> AddTeacher(PostTeacherViewModel model)
        {
            try
            {
                await _teacherRepo.AddTeacherAsync(model);

                if(await _teacherRepo.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500,"Något gick fel vid registrering av lärare");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);                
            }
        }
        [HttpPost("addcomp/{id}")]
        public async Task<ActionResult> AddCompetenceToTeacher(int id,PostCompetenceViewModel model)
        {
            try
            {
                await _teacherRepo.AddCompToTeacherAsync(id,model);

                if (await _teacherRepo.SaveAllAsync())
                {
                    return StatusCode(201);
                }

                return StatusCode(500,"Något gick fel vid registrering av kompetens");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("removecomp/{id}")]
        public async Task<ActionResult> RemoveCompetenceFromTeacher(int id,PostCompetenceViewModel model)
        {
            try
            {
                await _teacherRepo.RemoveCompFromTeacherAsync(id,model);

                if (await _teacherRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel vnär kompetensen skulle tas bort från läraren");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("addcourse/{id}")]
        public async Task<ActionResult> AddTeacherToCourse(int id, CourseSearchModel model)
        {
            try
            {
                await _teacherRepo.AddTeacherToCourseAsync(id,model);

                if(await _teacherRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick vid registrering av lärare till kurs");
            }
            catch (Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacher(int id,PostTeacherViewModel model)
        {
            try
            {
                await _teacherRepo.UpdateTeacherAsync(id,model);

                if (await _teacherRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel vid updatering av lärare");

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            try
            {
                await _teacherRepo.DeleteTeacherAsync(id);

                if(await _teacherRepo.SaveAllAsync())
                {
                    return NoContent();
                }

                return StatusCode(500,"Något gick fel borttagande av lärare");

            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }

    }
}