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
        private readonly string _baseUrl = "https://localhost:7113";

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

        protected async Task<T?> PostAsync<T>(string Path, T Body)
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

            var content = JsonConvert.SerializeObject(Body);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage Res = await client.PostAsync(Path, httpContent);

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(EmpResponse);

                return result;
            }

            return default;
        }

        protected async Task<T?> PutAsync<T>(string Path, T Body)
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

            var content = JsonConvert.SerializeObject(Body);
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");
            
            HttpResponseMessage Res = await client.PutAsync(Path, httpContent);

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(EmpResponse);

                return result;
            }

            return default;
        }

        protected async Task<T?> PutAsync<T>(string Path)
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

            HttpContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

            HttpResponseMessage Res = await client.PutAsync(Path, httpContent);

            if (Res.IsSuccessStatusCode)
            {
                var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                T result = JsonConvert.DeserializeObject<T>(EmpResponse);

                return result;
            }

            return default;
        }

        protected async Task<bool> DeleteAsync<T>(string Path)
        {
            using var client = new HttpClient();

            var baseUri = new Uri(_baseUrl);

            client.BaseAddress = baseUri;

            HttpResponseMessage Res = await client.DeleteAsync(Path);

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
