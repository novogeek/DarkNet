using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Darknet.Utilities
{
    public class HttpHelper : IHttpHelper
    {
        HttpClient httpClient;
        public HttpHelper()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<V> PostAsync<T, V>(string uri, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                String responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<V>(responseString);
            }
            else
            {
                return default(V);
            }
        }

        public async Task<V> GetAsync<V>(string uri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                String responseString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<V>(responseString);
            }
            else
            {
                return default(V);
            }
        }
    }
}
