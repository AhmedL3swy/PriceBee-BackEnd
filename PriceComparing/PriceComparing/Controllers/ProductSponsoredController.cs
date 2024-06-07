﻿using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductSponsoredController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public ProductSponsoredController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductSponsoreds()
		{
			var productSponsoreds = await _unitOfWork.ProductSponsoredRepository.SelectAll();
			if (productSponsoreds == null) return NotFound();

			List<ProductSponsoredDTO> productSponsoredDTOs = new List<ProductSponsoredDTO>();
			foreach (var productSponsored in productSponsoreds)
			{
				productSponsoredDTOs.Add(new ProductSponsoredDTO()
				{
					Id = productSponsored.Id,
					Cost = productSponsored.Cost,
					StartDate = productSponsored.StartDate,
					Duration = productSponsored.Duration,
					//ProdId = productSponsored.ProdId
				});
			}
			return Ok(productSponsoredDTOs);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductSponsoredById(int id)
		{
			var productSponsored = await _unitOfWork.ProductSponsoredRepository.SelectById(id);
			if (productSponsored == null) return NotFound();

			ProductSponsoredDTO productSponsoredDTO = new ProductSponsoredDTO()
			{
				Id = productSponsored.Id,
				Cost = productSponsored.Cost,
				StartDate = productSponsored.StartDate,
				Duration = productSponsored.Duration,
				//ProdId = productSponsored.ProdId
			};
			return Ok(productSponsoredDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddProductSponsored(ProductSponsoredPostDTO productSponsoredDTO)
		{
			if (productSponsoredDTO == null) return BadRequest();

			ProductSponsored productSponsored = new ProductSponsored()
			{
				Cost = productSponsoredDTO.Cost,
				StartDate = productSponsoredDTO.StartDate,
				Duration = productSponsoredDTO.Duration,
				//ProdId = productSponsoredDTO.ProdId
			};
			await _unitOfWork.ProductSponsoredRepository.Add(productSponsored);
			_unitOfWork.savechanges();
			return Ok(productSponsored);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProductSponsored(int id, [FromBody] ProductSponsoredPostDTO productSponsoredDTO)
		{
			if (productSponsoredDTO == null) return BadRequest();
			var productSponsored = await _unitOfWork.ProductSponsoredRepository.SelectById(id);
			if (productSponsored == null) return NotFound();

			productSponsored.Cost = productSponsoredDTO.Cost;
			productSponsored.StartDate = productSponsoredDTO.StartDate;
			productSponsored.Duration = productSponsoredDTO.Duration;
			//productSponsored.ProdId = productSponsoredDTO.ProdId;

			await _unitOfWork.ProductSponsoredRepository.UpdateAsync(productSponsored);
			return Ok(productSponsored);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductSponsored(int id)
		{
			var productSponsored = await _unitOfWork.ProductSponsoredRepository.SelectById(id);
			if (productSponsored == null) return NotFound();

			await _unitOfWork.ProductSponsoredRepository.Delete(id);
			return Ok();
		}
	}
}
