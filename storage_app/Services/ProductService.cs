using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

using storage_app.Models;

namespace storage_app.Services
{
    internal class ProductService : IProductService
    {
        private string _baseUrl = "https://localhost:7113";

        public async Task<List<Product>> GetProducts()
        {
            List<Product> products = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("v1/products");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    products = JsonConvert.DeserializeObject<List<Product>>(EmpResponse);

                    return products;
                }

            }
            return null;
        }

        public async Task<Product> GetProductById(int Id)
        {
            Product product = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync(String.Concat("v1/products/", Id));
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    product = JsonConvert.DeserializeObject<Product>(EmpResponse);

                    return product;
                }

            }
            return null;
        }
        public async Task<bool> InsertProduct(Product prod)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var myContent = JsonConvert.SerializeObject(prod);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                HttpResponseMessage Res = await client.PostAsync("/products", byteContent);
                if (Res.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            return false;
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
