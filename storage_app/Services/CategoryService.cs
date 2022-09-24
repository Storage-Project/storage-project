using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

using storage_app.Models;

namespace storage_app.Services
{
    internal class CategoryService : ICategoryService
    {
        private string _baseUrl = "https://estoque-api.azurewebsites.net";
        public async Task<List<Category>> GetCategories()
        {
            List<Category> categories = new List<Category>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("v1/categories");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    categories = JsonConvert.DeserializeObject<List<Category>>(EmpResponse);

                    return categories;
                }

            }
            return null;
        }
    }
}
