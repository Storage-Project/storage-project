using System;
using System.Collections.Generic;
using storage.Models;

namespace storage.Repository
{
    public interface IProductReporitory
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByID(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productID);
        void UpdateProduct(Product product);
        
    }
}