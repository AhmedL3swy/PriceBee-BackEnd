using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO;
using DataAccess.Models;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		UnitOfWOrks _UnitOfWork;

		public ProductController(UnitOfWOrks unitOfWork)
		{
			_UnitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts()
		{
			var products = await _UnitOfWork.ProductRepository.SelectAll();
			if (products == null) { return NotFound(); }
			List<ProductDTO> productsDTO = new List<ProductDTO>();
			foreach (var product in products)
			{
				productsDTO.Add(new ProductDTO()
				{
					Id = product.Id,
					Name_Local = product.Name_Local,
					Name_Global = product.Name_Global,
					Description_Local = product.Description_Local,
					Description_Global = product.Description_Global,
					SubCategoryId = product.SubCategoryId,
					BrandId = product.BrandId
				});
			}
			return Ok(productsDTO);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var product = await _UnitOfWork.ProductRepository.SelectById(id);
			if (product == null) { return NotFound(); }
			ProductDTO productDTO = new ProductDTO()
			{
				Id = product.Id,
				Name_Local = product.Name_Local,
				Name_Global = product.Name_Global,
				Description_Local = product.Description_Local,
				Description_Global = product.Description_Global,
				SubCategoryId = product.SubCategoryId,
				BrandId = product.BrandId
			};
			return Ok(productDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(ProductPostDTO productDTO)
		{
			if (productDTO == null) { return BadRequest(); }
			Product product = new Product()
			{
				Name_Local = productDTO.Name_Local,
				Name_Global = productDTO.Name_Global,
				Description_Local = productDTO.Description_Local,
				Description_Global = productDTO.Description_Global,
				SubCategoryId = productDTO.SubCategoryId,
				BrandId = productDTO.BrandId
			};
			await _UnitOfWork.ProductRepository.Add(product);
			_UnitOfWork.savechanges();
			return Ok(product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductPostDTO productDTO)
		{
			if (productDTO == null) { return BadRequest(); }
			var product = await _UnitOfWork.ProductRepository.SelectById(id);
			if (product == null) { return NotFound(); }

			product.Name_Local = productDTO.Name_Local;
			product.Name_Global = productDTO.Name_Global;
			product.Description_Local = productDTO.Description_Local;
			product.Description_Global = productDTO.Description_Global;
			product.SubCategoryId = productDTO.SubCategoryId;
			product.BrandId = productDTO.BrandId;

			await _UnitOfWork.ProductRepository.UpdateAsync(product);
			return Ok(product);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var product = await _UnitOfWork.ProductRepository.SelectById(id);
			if (product == null) { return NotFound(); }

			await _UnitOfWork.ProductRepository.Delete(id);
			return Ok();
		}




	}
}
