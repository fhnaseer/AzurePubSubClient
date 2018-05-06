using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubSub.Model
{
    public static class HttpRestClient
    {
        public static async Task<string> Post(string url, object postObject = null)
        {
            var content = JsonConvert.SerializeObject(postObject);
            var client = new HttpClient { Timeout = TimeSpan.FromMinutes(1) };
            var response = await client.PostAsync(url, new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> Get(string url)
        {
            var client = new HttpClient { Timeout = TimeSpan.FromMinutes(1) };
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
