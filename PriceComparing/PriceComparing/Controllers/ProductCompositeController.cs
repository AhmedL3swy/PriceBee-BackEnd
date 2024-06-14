using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CombinedProductController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public CombinedProductController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetCombinedProductDetails()
		{
			// Load products with related data using Eager Loading
			var products = await _unitOfWork.ProductRepository
				.SelectAllProduct()
				.Include(p => p.SubCategory)
				.Include(p => p.Brand)
				.Include(p => p.ProductImages)
				.Include(p => p.ProductLinks)
					.ThenInclude(pl => pl.Domain)
				.Include(p => p.ProductLinks)
					.ThenInclude(pl => pl.ProductDetail)
				.ToListAsync();

			if (products == null || !products.Any()) return NotFound();

			var combinedProductDetails = new List<CombinedProductDetailDTO>();

			foreach (var product in products)
			{
				var combinedProductDetail = new CombinedProductDetailDTO
				{
					ProductId = product.Id,
					ProductName_Global = product.Name_Global,
					ProductDescription_Global = product.Description_Global,
					SubCategoryName = product.SubCategory?.Name_Global,
					BrandName = product.Brand?.Name_Global,
					// Ensure non-empty lists are handled
					LastUpdated = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastUpdated) : DateTime.MinValue,
					LastScraped = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastScraped) : DateTime.MinValue,
					Images = product.ProductImages.Select(img => img.Image).ToList(),
					Links = product.ProductLinks.Select(link => new ProductLinkDTO2
					{
						DomainName = link.Domain.Name_Global,
						DomainLogo = link.Domain.Logo,
						ProductLink = link.ProductLink1,
						Price = link.ProductDetail?.Price ?? 0,
						Rating = link.ProductDetail?.Rating
					}).ToList()
				};
				Console.WriteLine($"Product: {product.Name_Global}, Images: {combinedProductDetail.Images.Count}, Links: {combinedProductDetail.Links.Count}");

				combinedProductDetails.Add(combinedProductDetail);
			}

			return Ok(combinedProductDetails);
		}

        // Delete product, product detials and related data
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var prodcut = await _unitOfWork.ProductRepository.SelectById(id);
            if (prodcut == null) return NotFound();

            // Delete related with the user (UserFavProd, UserAlertProd, UserHistoryProd)
            // User Favorite Products
            // await _unitOfWork.

            // User Alert Products
            await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserAlertProd);
            // User History Products
            await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserHistoryProd);
            // User Favorite Products
			await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserFavProd);

            // Delete related data
            await _unitOfWork.PriceHistoryRepository.DeleteRange(prodcut.PriceHistories);
            await _unitOfWork.ProductImageRepository.DeleteRange(prodcut.ProductImages);
            await _unitOfWork.ProductDetailRepository.DeleteRange(prodcut.ProductLinks.Select(pl => pl.ProductDetail));
			await _unitOfWork.ProductLinkRepository.DeleteRange(prodcut.ProductLinks);



			await _unitOfWork.ProductRepository.Delete(prodcut.Id);



            return Ok();
        }

    }
}
