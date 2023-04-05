using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApi.Interfaces;
using EducationApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EducationApi.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
            
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CategoryViewModel>>> ListAllCompetencies()
        {
            return Ok(await _categoryRepo.ListAllCategoriesAsync());
        }
    }
}