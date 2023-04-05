using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EducationApi.Interfaces;
using EducationApi.ViewModels.Competence;
using Microsoft.AspNetCore.Mvc;

namespace EducationApi.Controllers
{
    [ApiController]
    [Route("api/competencies")]
    public class CompetenceController : ControllerBase
    {
        private readonly ICompetenceRepository _competenceRepo;
       
        public CompetenceController(ICompetenceRepository competenceRepo)
        {
            _competenceRepo = competenceRepo;
          
        }
        [HttpGet("list")]
        public async Task<ActionResult<List<CompetenceViewModel>>> ListAllCompetencies()
        {
            return Ok(await _competenceRepo.ListAllCompetenciesAsync());
        }
    }
}