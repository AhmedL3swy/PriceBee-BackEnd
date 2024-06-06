using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductDetailController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public ProductDetailController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductDetails()
		{
			var productDetails = await _unitOfWork.ProductDetailRepository.SelectAll();
			if (productDetails == null) return NotFound();

			List<ProductDetailDTO> productDetailDTOs = new List<ProductDetailDTO>();
			foreach (var productDetail in productDetails)
			{
				productDetailDTOs.Add(new ProductDetailDTO()
				{
					Id = productDetail.Id,
					Name_Local = productDetail.Name_Local,
					Name_Global = productDetail.Name_Global,
					Description_Local = productDetail.Description_Local,
					Description_Global = productDetail.Description_Global,
					Price = productDetail.Price,
					Rating = productDetail.Rating,
					isAvailable = productDetail.isAvailable,
					Brand = productDetail.Brand
				});
			}
			return Ok(productDetailDTOs);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductDetailById(int id)
		{
			var productDetail = await _unitOfWork.ProductDetailRepository.SelectById(id);
			if (productDetail == null) return NotFound();

			ProductDetailDTO productDetailDTO = new ProductDetailDTO()
			{
				Id = productDetail.Id,
				Name_Local = productDetail.Name_Local,
				Name_Global = productDetail.Name_Global,
				Description_Local = productDetail.Description_Local,
				Description_Global = productDetail.Description_Global,
				Price = productDetail.Price,
				Rating = productDetail.Rating,
				isAvailable = productDetail.isAvailable,
				Brand = productDetail.Brand
			};
			return Ok(productDetailDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductDetail(ProductDetailPostDTO productDetailDTO)
		{
			if (productDetailDTO == null) return BadRequest();

			ProductDetail productDetail = new ProductDetail()
			{
				Name_Local = productDetailDTO.Name_Local,
				Name_Global = productDetailDTO.Name_Global,
				Description_Local = productDetailDTO.Description_Local,
				Description_Global = productDetailDTO.Description_Global,
				Price = productDetailDTO.Price,
				Rating = productDetailDTO.Rating,
				isAvailable = productDetailDTO.isAvailable,
				Brand = productDetailDTO.Brand
			};
			await _unitOfWork.ProductDetailRepository.Add(productDetail);
			_unitOfWork.savechanges();
			return Ok(productDetail);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductDetail(int id, [FromBody] ProductDetailPostDTO productDetailDTO)
		{
			if (productDetailDTO == null) return BadRequest();
			var productDetail = await _unitOfWork.ProductDetailRepository.SelectById(id);
			if (productDetail == null) return NotFound();

			productDetail.Name_Local = productDetailDTO.Name_Local;
			productDetail.Name_Global = productDetailDTO.Name_Global;
			productDetail.Description_Local = productDetailDTO.Description_Local;
			productDetail.Description_Global = productDetailDTO.Description_Global;
			productDetail.Price = productDetailDTO.Price;
			productDetail.Rating = productDetailDTO.Rating;
			productDetail.isAvailable = productDetailDTO.isAvailable;
			productDetail.Brand = productDetailDTO.Brand;

			await _unitOfWork.ProductDetailRepository.UpdateAsync(productDetail);
			return Ok(productDetail);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductDetail(int id)
		{
			var productDetail = await _unitOfWork.ProductDetailRepository.SelectById(id);
			if (productDetail == null) return NotFound();

			await _unitOfWork.ProductDetailRepository.Delete(id);
			return Ok();
		}
	}
}
