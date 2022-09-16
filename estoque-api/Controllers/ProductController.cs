using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using storage.Dto;
using storage.Models;
using storage.Repository;

namespace storage.Controllers{

    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase {

        ProductRepository _repository;
        CategoryRepository _categoryRepository;

        public ProductController([FromServices] AppDbContext context){
            _repository = new ProductRepository(context);
            _categoryRepository = new CategoryRepository(context);
        }

        [HttpGet]
        [Route("products")]
        public async Task<IActionResult> GetAsync(){
            var response = _repository.GetProducts();
            if(response == null){
                return StatusCode(500);
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id){

            var product =  _repository.GetProductByID(id);
            if (product==null)
                return NotFound();
            return Ok(product);
            

        }

        [HttpGet]
        [Route("products/filters")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromQuery] string? description,  [FromQuery] string? category, [FromQuery] int? quantity){
            var _products = context.Products;
            if (_products == null)
                return StatusCode(500);
            var products =  await _products.AsNoTracking().Include(product => product.Category)
            .Where(x => description==null? true : x.Description.Contains(description))
            .Where(x => category==null ? true : x.Category.Description.Contains(category))
            .Where(x => quantity==null ? true : x.Quantity <= quantity)

            .ToListAsync();
    
            if (products==null)
                return NotFound();
            return Ok(products);

        }

        [HttpPost("products")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateProduct product){
            var _products = context.Products;
            var _categories = context.Categories;
            if (_products == null || _categories == null)
                return StatusCode(500);
            //aplica validações (required por ex)
            if (!ModelState.IsValid){
                return BadRequest("data is invalid");
            }
            //var category = (from c in context.Categories where c.Description == product.Category.Description select c).FirstOrDefault();
            var category = _categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
            if (category == null){
                category = product.Category;
            }
            var prod = new Product{
                Description = product.Description,
                Category = category,
                Price = product.Price,
                Quantity = product.Quantity,
                Create_at = DateTimeOffset.Now
            };
            try{
                await _products.AddAsync(prod);
                await context.SaveChangesAsync();
                return Created($"v1/products/{prod.Id}", prod);
            }catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] UpdateProduct product, [FromRoute] int id){
            var _products = context.Products;
            var _categories = context.Categories;
            if (_products == null || _categories == null)
                return StatusCode(500);
            if (!ModelState.IsValid){
                return BadRequest();
            }
            var prod = await _products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null){
                return NotFound();
            }

            var category = _categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
            if (category == null){
                category = product.Category;
            }

            try{
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.Category = category;

                _products.Update(prod);
                await context.SaveChangesAsync();
                return Ok(prod);
            }catch{
                return StatusCode(500);
            }

        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id){
            var _products = context.Products;
            if (_products == null)
                return StatusCode(500);
            var prod = await _products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null){
                return NotFound();
            }
            try{
                _products.Remove(prod);
                await context.SaveChangesAsync();
                return Ok();
            }catch{
                return StatusCode(500);
            }

        }
    
        [HttpPut("products/sell/{id}/{quantity}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromRoute] int id, [FromRoute] int quantity){
            var _products = context.Products;
            if (_products == null)
                return StatusCode(500);
            var prod = await _products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null){
                return NotFound();
            }

            try{
                if ((prod.Quantity - quantity) >= 0){
                    prod.Quantity = prod.Quantity - quantity;
                }else{
                    return BadRequest("unable to sell this quantity");
                }

                _products.Update(prod);
                await context.SaveChangesAsync();
                return Ok(prod);
            }catch{
                return StatusCode(500);
            }

        }

    }
}