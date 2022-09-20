using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using storage.Exceptions;
using storage.Repository;
using storage.Models;

namespace storage.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = await _categoryRepository.GetCategories();
                return Ok(response);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("categories/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByID(id);
                if (category == null)
                    return NoContent();
                return Ok(category);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("categories")]
        public async Task<IActionResult> PostAsync([FromBody] Category category)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var cat = await _categoryRepository.InsertCategory(category);
                return Created($"v1/categories/{cat.Id}", cat);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("categories/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] Category category, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var cat = await _categoryRepository.UpdateCategory(category, id);
                if (cat == null)
                    return NotFound();
                return Ok(cat);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

         [HttpDelete("categories/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var result = false;
            try
            {
                result = await _categoryRepository.DeleteCategory(id);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }

            if (result == true)
                return Ok();
            else
                return NotFound();
        }
    }
}