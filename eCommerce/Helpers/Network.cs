using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace eCommerce.Helpers
{
    public class Network
    {
        const string API_URL = "https://ecommerce.mocklab.io/";

        public static async Task<T> GetFromAPI<T>(string urlExtension)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(API_URL + urlExtension);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();

                    return (T)await Task.FromResult(JsonConvert.DeserializeObject<T>(result));
                }
                else
                {
                    throw new HttpRequestException($"Error accessing API. Code: {response.StatusCode}");
                }
            }
        }
    }
}
