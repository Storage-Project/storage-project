using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using storage.Dto;
using storage.Models;
using storage.Exceptions;


namespace storage.Repository
{
    public class ProductRepository : IProductReporitory
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
            {
                return false;
            }
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


        public async Task<Product> GetProductByID(int productId)
        {
            var _products = _context.Products;
            if (_products != null)
            {
                return await _products.AsNoTracking().Include(product => product.Category).FirstOrDefaultAsync(x => x.Id == productId);
            }
            else
            {
                throw new InternalServerError();
            }
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var _products = _context.Products;
            if (_products != null)
            {
                return await _products.AsNoTracking().Include(product => product.Category).ToListAsync();
            }
            else
            {
                throw new InternalServerError();
            }
        }

        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
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