using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace storage_app.Services
{
    internal class ServiceBase
    {
        private readonly string _baseUrl = "https://estoque-api.azurewebsites.net";

        protected async Task<T?> GetValueAsync<T>(string Path, Dictionary<string,string>? query = null)
        {
            using var client = new HttpClient();

            string filledPath = BuilPathWithQuery(Path, query);

            client
                .DefaultRequestHeaders
                .Clear();

            client
                .DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await client.GetAsync(filledPath);

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

        private string BuilPathWithQuery(string path, Dictionary<string, string>? query)
        {
            var builder = new UriBuilder(_baseUrl)
            {
                Path = path
            };

            if (query == null) return builder.ToString();

            var queryCollection = HttpUtility.ParseQueryString(string.Empty);
            foreach (var keyValuePair in query)
            {
                queryCollection.Add(keyValuePair.Key, keyValuePair.Value);
            }
            builder.Query = queryCollection.ToString();
            return builder.ToString();
        }
    }
}
