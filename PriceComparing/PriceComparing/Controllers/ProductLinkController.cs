using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductLinkController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public ProductLinkController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductLinks()
		{
			var productLinks = await _unitOfWork.ProductLinkRepository.SelectAll();
			if (productLinks == null) return NotFound();

			List<ProductLinkDTO> productLinkDTOs = new List<ProductLinkDTO>();
			foreach (var productLink in productLinks)
			{
				productLinkDTOs.Add(new ProductLinkDTO()
				{
					Id = productLink.Id,
					ProdId = productLink.ProdId,
					DomainId = productLink.DomainId,
					ProductLink1 = productLink.ProductLink1,
					Status = productLink.Status,
					LastUpdated = productLink.LastUpdated,
					LastScraped = productLink.LastScraped
				});
			}
			return Ok(productLinkDTOs);
		}

		// GET: api/ProductLink/All
		[HttpGet("All")]
        public async Task<IActionResult> GetAllProductLinksIgnoringFilters()
		{
            var productLinks = await _unitOfWork.ProductLinkRepository.SelectAllIgnoringFiltersAsync();
            if (productLinks == null) return NotFound();

            List<ProductLinkDTO> productLinkDTOs = new List<ProductLinkDTO>();
            foreach (var productLink in productLinks)
            {
                productLinkDTOs.Add(new ProductLinkDTO()
                {
                    Id = productLink.Id,
                    ProdId = productLink.ProdId,
                    DomainId = productLink.DomainId,
                    ProductLink1 = productLink.ProductLink1,
                    Status = productLink.Status,
                    LastUpdated = productLink.LastUpdated,
                    LastScraped = productLink.LastScraped
                });
            }
            return Ok(productLinkDTOs);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetProductLinkById(int id)
		{
			var productLink = await _unitOfWork.ProductLinkRepository.SelectById(id);
			if (productLink == null) return NotFound();

			ProductLinkDTO productLinkDTO = new ProductLinkDTO()
			{
				Id = productLink.Id,
				ProdId = productLink.ProdId,
				DomainId = productLink.DomainId,
				ProductLink1 = productLink.ProductLink1,
				Status = productLink.Status,
				LastUpdated = productLink.LastUpdated,
				LastScraped = productLink.LastScraped
			};
			return Ok(productLinkDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductLink(ProductLinkPostDTO productLinkDTO)
		{
			if (productLinkDTO == null) return BadRequest();

			ProductLink productLink = new ProductLink()
			{
				ProdId = productLinkDTO.ProdId,
				DomainId = productLinkDTO.DomainId,
				ProductLink1 = productLinkDTO.ProductLink1,
				Status = productLinkDTO.Status,
				LastUpdated = productLinkDTO.LastUpdated,
				LastScraped = productLinkDTO.LastScraped
			};
			await _unitOfWork.ProductLinkRepository.Add(productLink);
			_unitOfWork.savechanges();
			return Ok(productLink);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductLink(int id, [FromBody] ProductLinkPostDTO productLinkDTO)
		{
			if (productLinkDTO == null) return BadRequest();
			var productLink = await _unitOfWork.ProductLinkRepository.SelectById(id);
			if (productLink == null) return NotFound();

			productLink.ProdId = productLinkDTO.ProdId;
			productLink.DomainId = productLinkDTO.DomainId;
			productLink.ProductLink1 = productLinkDTO.ProductLink1;
			productLink.Status = productLinkDTO.Status;
			productLink.LastUpdated = productLinkDTO.LastUpdated;
			productLink.LastScraped = productLinkDTO.LastScraped;

			await _unitOfWork.ProductLinkRepository.UpdateAsync(productLink);
			return Ok(productLink);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductLink(int id)
		{
			var productLink = await _unitOfWork.ProductLinkRepository.SelectById(id);
			if (productLink == null) return NotFound();

			await _unitOfWork.ProductLinkRepository.Delete(id);
			return Ok();
		}
	}
}
