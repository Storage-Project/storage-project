using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using storage.ViewModels;
using storage.Models;

namespace storage.Controllers{

    [ApiController]
    [Route("v1")]
    public class ProductController : ControllerBase{

        //por padrao se nao deixar  nada ele entende como get
        [HttpGet]
        //ele vai concatenar e a ur vai ser v1/products
        [Route("products")]
        //dependency injection - pegar tudo que esta no services, pegando o AppDbCOntext
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context){
            var products =  await context.Products.AsNoTracking().Include(product => product.Category).ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context, [FromRoute] int id){
            var product =  await context.Products.AsNoTracking().Include(product => product.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (product==null)
                return NotFound();
            return Ok(product);
            

        }

        [HttpGet]
        //products?description={description} (description included auto)
        [Route("products/filters")]
        public async Task<IActionResult> GetAsync( [FromServices] AppDbContext context, [FromQuery] string? description,  [FromQuery] string? category){
            var products =  await context.Products.AsNoTracking().Where(x => description==null? true : x.Description.Contains(description)).Include(product => product.Category).Where(x => category==null ? true : x.Category.Description.Contains(category)).ToListAsync();
    
            if (products==null)
                return NotFound();
            return Ok(products);

        }

        [HttpPost("products")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateProduct product){
            //aplica validações (required por ex)
            if(!ModelState.IsValid){
                return BadRequest();
            }
            //var category = (from c in context.Categories where c.Description == product.Category.Description select c).FirstOrDefault();
            var category = context.Categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
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
                await context.Products.AddAsync(prod);
                await context.SaveChangesAsync();
                return Created($"v1/products/{prod.Id}", prod);
            }catch(Exception e){
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] UpdateProduct product, [FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest();
            }
            var prod = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null){
                return NotFound();
            }

            var category = context.Categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
            if (category == null){
                category = product.Category;
            }

            try{
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.Category = category;

                context.Products.Update(prod);
                await context.SaveChangesAsync();
                return Ok(prod);
            }catch{
                return BadRequest();
            }

        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id){
            var prod = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null){
                return NotFound();
            }
            try{
                context.Products.Remove(prod);
                await context.SaveChangesAsync();
                return Ok();
            }catch{
                return BadRequest();
            }

        }


    }
}