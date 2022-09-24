using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using storage_app.Models;

namespace storage_app.Services
{
    internal class ProductService : ServiceBase, IProductService
    {

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = new();

            var _products = await GetValueAsync<List<Product>>("v1/products");

            if (_products != null) 
                products = _products;

            return products;
        }

        public async Task<Product?> GetProductById(int Id)
        {
            Product? product = null;
            string Path = String.Concat("v1/products/", Id);

            var _product = await GetValueAsync<Product>(Path);

            if (_product != null)
                product = _product;

            return product;
        }

        public async Task<bool> InsertProduct(Product product)
        {
            return await PostAsync<Product>("/products", product);
        }

        public Task<Product> GetProductByDescription(string Name)
        {
            throw new NotImplementedException();
        }

        void IProductService.InsertProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }
    }
}