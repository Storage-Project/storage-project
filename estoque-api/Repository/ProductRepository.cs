using Microsoft.EntityFrameworkCore;

using storage.Dto;
using storage.Models;
using storage.Exceptions;


namespace storage.Repository
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            this._context = context;
        }
        public async Task<bool> DeleteProduct(int productID)
        {
            var _products = _context.Products;
            if (_products == null)
                throw new InternalServerError();
            var prod = await _products.FirstOrDefaultAsync(x => x.Id == productID);
            if (prod == null)
                return false;
            try
            {
                _products.Remove(prod);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw new InternalServerError();
            }
        }


        public async Task<Product?> GetProductByID(int productId)
        {
            var _products = _context.Products;
            if (_products != null)
                return await _products.AsNoTracking().Include(product => product.Category).FirstOrDefaultAsync(x => x.Id == productId);
            else
                throw new InternalServerError();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var _products = _context.Products;
            if (_products != null)
                return await _products.AsNoTracking().Include(product => product.Category).ToListAsync();
            else
                throw new InternalServerError();
        }

        public async Task<Product> InsertProduct(CreateProduct product)
        {
            var _products = _context.Products;
            var _categories = _context.Categories;
            if (_products == null || _categories == null)
                throw new InternalServerError();

            var category = _categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
            if (category == null)
                category = product.Category;

            var prod = new Product
            {
                Description = product.Description,
                Category = category,
                Price = product.Price,
                Quantity = product.Quantity,
                Create_at = DateTimeOffset.Now
            };
            try
            {
                await _products.AddAsync(prod);
                await _context.SaveChangesAsync();
                return prod;
            }
            catch (Exception)
            {
                throw new InternalServerError();
            }
        }

        public async Task<Product?> UpdateProduct(UpdateProduct product, int id)
        {
            var _products = _context.Products;
            var _categories = _context.Categories;
            if (_products == null || _categories == null)
                throw new InternalServerError();

            var prod = await _products.FirstOrDefaultAsync(x => x.Id == id);
            if (prod == null)
                return null;

            var category = _categories.Where(c => c.Description.Equals(product.Category.Description)).FirstOrDefault();
            if (category == null)
                category = product.Category;

            try
            {
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Quantity = product.Quantity;
                prod.Category = category;
                
                _products.Update(prod);
                await _context.SaveChangesAsync();
                return prod;
            }
            catch
            {
                throw new InternalServerError();
            }

        }

        public async Task<IEnumerable<Product>> GetByFilters(string? description, string? category, int? quantity)
        {
            var _products = _context.Products;
            if (_products != null)
            {
                return await _products.AsNoTracking().Include(product => product.Category)
                .Where(x => description == null ? true : x.Description.Contains(description))
                .Where(x => category == null ? true : x.Category.Description.Contains(category))
                .Where(x => quantity == null ? true : x.Quantity <= quantity)
                .ToListAsync();

            }
            else
            {
                throw new InternalServerError();
            }
        }

    }
}