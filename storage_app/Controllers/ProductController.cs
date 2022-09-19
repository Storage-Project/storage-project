using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

using storage_app.Models;

namespace storage_app.Controllers
{
    internal class ProductController
    {
        private string _baseUrl = "https://estoque-api.azurewebsites.net";

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
    }
}
