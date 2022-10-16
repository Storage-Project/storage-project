using System;
using System.Collections.Generic;
using storage.Models;
using storage.Dto;

namespace storage.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product?> GetProductByID(int productId);
        Task<Product> InsertProduct(CreateProduct product);
        Task<bool> DeleteProduct(int productID);
        Task<Product?> UpdateProduct(UpdateProduct product, int id);
        Task<IEnumerable<Product>> GetByFilters(int? id, string? description, string? category, int? quantity);
    }
}