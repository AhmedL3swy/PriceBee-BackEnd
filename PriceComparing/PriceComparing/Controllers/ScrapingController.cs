using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Services;

namespace PriceComparing.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ScrapingController : ControllerBase
    {
        private readonly ScrapingService _scrapingService;
        public ScrapingController(ScrapingService scrapingService)
        {
            _scrapingService = scrapingService;
        }

        [HttpGet("Noonprice")]
        public async Task<string> GetPrice(string url)
        {
            return await _scrapingService.Get("price/noon", url);
        }
        [HttpGet("Noonimages")]
        public async Task<string> GetImages(string url)
        {
            return await _scrapingService.Get("images/noon", url);
        }
        [HttpGet("Noondetails")]
        public async Task<string> GetDetails(string url)
        {
            return await _scrapingService.Get("details/noon", url);
        }
    }
}
