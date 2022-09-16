using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using storage.Exceptions;


namespace storage.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : Controller
    {
        private CategoryRepository _categoryRepository;

        public CategoryController([FromServices] AppDbContext context)
        {
            _categoryRepository = new CategoryRepository(context);
        }

        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = _categoryRepository.GetCategories();
                return Ok(response);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }
    }
}