using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using storage.Exceptions;
using storage.Repository;

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
    }
}