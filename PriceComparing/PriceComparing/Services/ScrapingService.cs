using DTO;
using System.Net.Http;
using System.Text.Json;
namespace PriceComparing.Services
{
    
    public class ScrapingService
    {
        HttpClient client;
        public ScrapingService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("ScrapingClient");
        }

        public async Task<ScrapingDTO> Get(string api, string url)
        {
            var response = await client.GetAsync($"/{api}/?url={url}");

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStreamAsync();

            return JsonSerializer.Deserialize<ScrapingDTO>(jsonResponse);
        }
    }
}
