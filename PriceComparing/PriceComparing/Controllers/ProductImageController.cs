using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO;
using DataAccess.Models;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductImageController : ControllerBase
	{
		UnitOfWOrks _UnitOfWork;

		public ProductImageController(UnitOfWOrks unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductImages()
		{
			var productImages = await _UnitOfWork.ProductImageRepository.SelectAll();
			if (productImages == null) { return NotFound(); }
			List<ProductImageDTO> productImagesDTO = new List<ProductImageDTO>();
			foreach (var productImage in productImages)
			{
				productImagesDTO.Add(new ProductImageDTO()
				{
					Id = productImage.Id,
					ProdId = productImage.ProdId,
					Image = productImage.Image
				});
			}
			return Ok(productImagesDTO);
		}

		// GET: api/ProductImage/All
		[HttpGet("All")]
        public async Task<IActionResult> GetAllProductImagesIgnoringFilters()
        {
            var productImages = await _UnitOfWork.ProductImageRepository.SelectAllIgnoringFiltersAsync();
            if (productImages == null) { return NotFound(); }
            List<ProductImageDTO> productImagesDTO = new List<ProductImageDTO>();
            foreach (var productImage in productImages)
            {
                productImagesDTO.Add(new ProductImageDTO()
                {
                    Id = productImage.Id,
                    ProdId = productImage.ProdId,
                    Image = productImage.Image
                });
            }
            return Ok(productImagesDTO);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetProductImageById(int id)
		{
			var productImage = await _UnitOfWork.ProductImageRepository.SelectById(id);
			if (productImage == null) { return NotFound(); }
			ProductImageDTO productImageDTO = new ProductImageDTO()
			{
				Id = productImage.Id,
				ProdId = productImage.ProdId,
				Image = productImage.Image
			};
			return Ok(productImageDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductImage(ProductImagePostDTO productImageDTO)
		{
			if (productImageDTO == null) { return BadRequest(); }
			ProductImage productImage = new ProductImage()
			{
				ProdId = productImageDTO.ProdId,
				Image = productImageDTO.Image
			};
			await _UnitOfWork.ProductImageRepository.Add(productImage);
			_UnitOfWork.savechanges();
			ProductImageDTO productImageUpdated = new ProductImageDTO()
			{
				Id = productImage.Id,
				ProdId = productImage.ProdId,
				Image = productImage.Image
			};
			return Ok(productImageUpdated);
			//return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductImage(int id, [FromBody] ProductImagePostDTO productImageDTO)
		{
			if (productImageDTO == null) { return BadRequest(); }
			var productImage = await _UnitOfWork.ProductImageRepository.SelectById(id);
			if (productImage == null) { return NotFound(); }

			productImage.ProdId = productImageDTO.ProdId;
			productImage.Image = productImageDTO.Image;

			await _UnitOfWork.ProductImageRepository.UpdateAsync(productImage);
			ProductImageDTO productImageUpdated = new ProductImageDTO()
			{
				Id = productImage.Id,
				ProdId = productImage.ProdId,
				Image = productImage.Image
			};
			return Ok(productImageUpdated);
			//return Ok(productImage);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductImage(int id)
		{
			var productImage = await _UnitOfWork.ProductImageRepository.SelectById(id);
			if (productImage == null) { return NotFound(); }

			await _UnitOfWork.ProductImageRepository.Delete(id);
			return Ok();
		}

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteProductImage(int id)
        {
            var productImage = await _UnitOfWork.ProductImageRepository.SelectById(id);
            if (productImage == null) { return NotFound(); }

            await _UnitOfWork.ProductImageRepository.SoftDelete(id);
            return Ok();
        }
    }
}
