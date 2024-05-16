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
        #region Noon
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
        #endregion

        #region AmazonSa
        [HttpGet("AmazonSaprice")]
        public async Task<string> GetAmazonSaPrice(string url)
        {
            return await _scrapingService.Get("price/amazonSa", url);
        }
        [HttpGet("AmazonSaimages")]
        public async Task<string> GetAmazonSaImages(string url)
        {
            return await _scrapingService.Get("images/amazonSa", url);
        }
        [HttpGet("AmazonSaDetails")]
        public async Task<string> GetAmazonSaDetails(string url)
        {
            return await _scrapingService.Get("details/amazonSa", url);
        }
        #endregion

        #region Amazon
        [HttpGet("Amazonprice")]
        public async Task<string> GetAmazonPrice(string url)
        {
            return await _scrapingService.Get("price/amazon", url);
        }
        [HttpGet("Amazonimages")]
        public async Task<string> GetAmazonImages(string url)
        {
            return await _scrapingService.Get("images/amazon", url);
        }
        [HttpGet("AmazonDetails")]
        public async Task<string> GetAmazonDetails(string url)
        {
            return await _scrapingService.Get("details/amazon", url);
        }
        #endregion
        #region Extra
        [HttpGet("Extraprice")]
        public async Task<string> GetExtraPrice(string url)
        {
            return await _scrapingService.Get("price/extra", url);
        }
        [HttpGet("Extraimages")]
        public async Task<string> GetExtraImages(string url)
        {
            return await _scrapingService.Get("images/extra", url);
        }
        [HttpGet("ExtraDetails")]
        public async Task<string> GetExtraDetails(string url)
        {
            return await _scrapingService.Get("details/extra", url);
        }
        #endregion
        #region Jarir
        [HttpGet("Jarirprice")]
        public async Task<string> GetJarirPrice(string url)
        {
            return await _scrapingService.Get("price/jarir", url);
        }
        [HttpGet("Jaririmages")]
        public async Task<string> GetJarirImages(string url)
        {
            return await _scrapingService.Get("images/jarir", url);
        }
        [HttpGet("JarirDetails")]
        public async Task<string> GetJarirDetails(string url)
        {
            return await _scrapingService.Get("details/jarir", url);
        }
        #endregion
        #region aliexpress
        [HttpGet("aliexpressprice")]
        public async Task<string> GetaliexpressPrice(string url)
        {
            return await _scrapingService.Get("price/aliexpress", url);
        }
        [HttpGet("aliexpressimages")]
        public async Task<string> GetaliexpressImages(string url)
        {
            return await _scrapingService.Get("images/aliexpress", url);
        }
        [HttpGet("aliexpressDetails")]
        public async Task<string> GetaliexpressDetails(string url)
        {
            return await _scrapingService.Get("details/aliexpress", url);
        }
        #endregion



    }
}
