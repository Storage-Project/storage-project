using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace storage_app.Services
{
    internal class ServiceBase
    {
        private readonly string _baseUrl = "https://estoque-api.azurewebsites.net";

        protected async Task<T?> GetValueAsync<T>(string Path)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(_baseUrl);

            client
                .DefaultRequestHeaders
                .Clear();

            client
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync(Path);

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(EmpResponse);

                return result;
            }

            return default;
        }

        protected async Task<bool> PostAsync<T>(string Path, T Body)
        {
            using var client = new HttpClient();

            client.BaseAddress = new Uri(_baseUrl);

            var content = JsonConvert.SerializeObject(Body);
            var buffer = Encoding.UTF8.GetBytes(content);
            var ByteContent = new ByteArrayContent(buffer);

            HttpResponseMessage Res = await client.PostAsync(Path, ByteContent);

            if (Res.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
