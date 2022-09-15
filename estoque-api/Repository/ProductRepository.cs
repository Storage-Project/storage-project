using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using storage.Dto;
using storage.Models;


namespace storage.Repository
{
    public class ProductRepository : IProductReporitory
    {
        private AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            this._context = context;
        }
        public void DeleteProduct(int productID)
        {
            throw new NotImplementedException();
        }


        public async Task<Product> GetProductByID(int productId)
        {
            var _products = _context.Products;
            if (_products != null){
                return await _products.AsNoTracking().Include(product => product.Category).FirstOrDefaultAsync(x => x.Id == productId);
            }
            return null;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var _products = _context.Products;
            if (_products != null){
                return await _products.AsNoTracking().Include(product => product.Category).ToListAsync();
            }
            return null;
        }

        public void InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetByFilters(string? description,  string? category, int? quantity){
            var _products = _context.Products;
            if (_products!=null){
                return await _products.AsNoTracking().Include(product => product.Category)
                .Where(x => description==null? true : x.Description.Contains(description))
                .Where(x => category==null ? true : x.Category.Description.Contains(category))
                .Where(x => quantity==null ? true : x.Quantity <= quantity)
                .ToListAsync();
                
            }
            return null;
        }
    }
}