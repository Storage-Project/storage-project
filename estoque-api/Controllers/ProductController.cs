using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using storage.Dto;
using storage.Models;
using storage.Repository;
using storage.Exceptions;

namespace storage.Controllers
{

    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase
    {

        private IProductRepository _repository;

        public ProductController(IProductRepository productReporitory)
        {
            _repository = productReporitory;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var response = await _repository.GetProducts();
                return Ok(response);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            try
            {
                var product = await _repository.GetProductByID(id);
                if (product == null)
                    return NoContent();
                return Ok(product);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("products/filters")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromQuery] int? id, [FromQuery] string? description, [FromQuery] string? category, [FromQuery] int? quantity)
        {
            try
            {
                var products = await _repository.GetByFilters(id, description, category, quantity);
                if (products == null)
                    return NotFound();
                return Ok(products);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("products")]
        public async Task<IActionResult> PostAsync([FromBody] CreateProduct product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var prod = await _repository.InsertProduct(product);
                return Created($"v1/products/{prod.Id}", prod);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] UpdateProduct product, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var prod = await _repository.UpdateProduct(product, id);
                if (prod == null)
                    return NotFound();
                return Ok(prod);
            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var result = false;
            try
            {
                result = await _repository.DeleteProduct(id);
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


        [HttpPut("products/sell/{id}/{quantity}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromRoute] int id, [FromRoute] int quantity)
        {
            try
            {
                var product = await _repository.GetProductByID(id);
                if (product == null)
                    return NotFound();

                if ((product.Quantity - quantity) >= 0) {
                    product.Quantity = product.Quantity - quantity;
                    product.SellingCount += 1;
                }
                else
                    return BadRequest("unable to sell this quantity");

                var to_update = new UpdateProduct
                {
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = product.Category,
                    SellingCount = product.SellingCount

                };
                var prod_updated = _repository.UpdateProduct(to_update, id);
                if (prod_updated == null)
                    return StatusCode(500);

                return Ok(prod_updated);

            }
            catch (InternalServerError)
            {
                return StatusCode(500);
            }
        }

    }
}