using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PubSub.Model
{
    public static class HttpRestClient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object")]
        public static async Task<string> Post(string address, object postObject = null)
        {
            var content = JsonConvert.SerializeObject(postObject);
            var client = new HttpClient { Timeout = TimeSpan.FromMinutes(1) };
            var response = await client.PostAsync(address, new StringContent(content, System.Text.Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public static async Task<string> Get(string address)
        {
            var client = new HttpClient { Timeout = TimeSpan.FromMinutes(1) };
            var response = await client.GetAsync(address);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
