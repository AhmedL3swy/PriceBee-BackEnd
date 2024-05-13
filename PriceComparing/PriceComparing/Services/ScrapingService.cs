namespace PriceComparing.Services
{
    public class ScrapingService
    {
        HttpClient client;
        public ScrapingService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("ScrapingClient");
        }

        public async Task<string> Get(string api , string url)
        {

            var response = await client.GetAsync($"/{api}/?url={url}");
            return await response.Content.ReadAsStringAsync(); ;
        }
    }
}
