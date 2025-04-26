using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKodul1.Context;
using MKodul1.Entity;
using MKodul1.Services.ServicesInterface;

namespace MKodul1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController( ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCategory(string categoryTitle)
        {
            await _categoryService.CreateCategory(categoryTitle);
            return NoContent();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }
    }
}
