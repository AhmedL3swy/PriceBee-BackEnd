using DTO;
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
        // get as ScrapingResult
        [HttpGet]
        public async Task<List<ScrapingDTO>> Get([FromQuery] string url)
        {
            List<ScrapingDTO> result = await _scrapingService.Get("SingleScrape", url);
            return result;
        }


    }
}
