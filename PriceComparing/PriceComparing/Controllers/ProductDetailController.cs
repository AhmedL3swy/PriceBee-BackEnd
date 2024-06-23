﻿using DataAccess.Models;
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

        // GET: api/ProductDetail/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllProductDetailsIgnoringFilters()
        {
            var productDetails = await _unitOfWork.ProductDetailRepository.SelectAllIgnoringFiltersAsync();
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
				Id= productDetailDTO.Id,
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

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteProductDetail(int id)
        {
            var productDetail = await _unitOfWork.ProductDetailRepository.SelectById(id);
            if (productDetail == null) return NotFound();

            await _unitOfWork.ProductDetailRepository.SoftDelete(id);
            return Ok();
        }
        [HttpGet("ProductDetailsComponent/{id}")]
        public async Task<IActionResult> GetProductDetailsByProductId(int id)
        {
            var productDetail = await _unitOfWork.ProductDetailRepository.SelectById(id);
            if (productDetail == null) return NotFound();

            // Assuming you have methods or ways to fetch these additional details
            // For demonstration, these are fetched directly. Adjust based on your actual data access strategy.
            var prices = new List<decimal>(); // Fetch prices for the product
            var images = new List<string>(); // Fetch image URLs or paths for the product
            var domains = new List<DomainDTO>(); // Convert or fetch domain data related to the product
            var productLinks = new List<ProductLinkDTO>(); // Convert or fetch product link data related to the product

            ProductDetailsComponentDetailsDTO productDetailDTO = new ProductDetailsComponentDetailsDTO
            {
                Id = productDetail.Id,
                Name_Local = productDetail.Name_Local,
                Name_Global = productDetail.Name_Global,
                Description_Local = productDetail.Description_Local,
                Description_Global = productDetail.Description_Global,
                Prices = prices, // Assuming you have a way to populate this list based on the product ID
                Rating = productDetail.Rating,
                isAvailable = productDetail.isAvailable,
                Brand = productDetail.Brand,
                Images = images, // Assuming you have a way to populate this list based on the product ID
                Domains = domains, // Populate this list with relevant domain data
                ProductLinks = productLinks // Populate this list with relevant product link data
            };

            return Ok(productDetailDTO);
        }



    }
}
