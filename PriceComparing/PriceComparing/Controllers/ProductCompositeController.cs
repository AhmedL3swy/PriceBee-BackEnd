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
                    ProductName_Local = product.Name_Local,
                    ProductName_Global = product.Name_Global,
                    ProductDescription_Local = product.Description_Local,
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


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCombinedProductDetailsByID(int id)
        {
            // Filter to load the specific product with related data using Eager Loading
            var product = await _unitOfWork.ProductRepository
                .SelectAllProduct()
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.Domain)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                .FirstOrDefaultAsync(p => p.Id == id); // Apply filter here

            if (product == null) return NotFound();

            // Calculate the minimum price among the product links
            var minPriceLink = product.ProductLinks
                .Where(pl => pl.ProductDetail != null)
                .OrderBy(pl => pl.ProductDetail.Price)
                .FirstOrDefault();

			var combinedProductDetail = new CombinedProductDetailDTO
			{
				ProductId = product.Id,
				ProductName_Local = product.Name_Local,
				ProductName_Global = product.Name_Global,
				ProductDescription_Local = product.Description_Local,
				ProductDescription_Global = product.Description_Global,
				SubCategoryName = product.SubCategory?.Name_Global,
				BrandName = product.Brand?.Name_Global,
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
				}).ToList(),
				// Add minimum price, domain logo, and brand name for the product with the minimum price
				MinPrice = minPriceLink?.ProductDetail?.Price ?? 0,
				MinPriceDomainLogo = minPriceLink?.Domain?.Logo,
				MinPriceBrandName = minPriceLink?.ProductDetail?.Brand
			};

            Console.WriteLine($"Product: {product.Name_Global}, Images: {combinedProductDetail.Images.Count}, Links: {combinedProductDetail.Links.Count}, MinPrice: {combinedProductDetail.MinPrice}");

            return Ok(combinedProductDetail);
        }



        // Delete product, product detials and related data
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var prodcut = await _unitOfWork.ProductRepository.SelectById(id);
            if (prodcut == null) return NotFound();

            // User Alert Products
            await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserAlertProds);
            // User History Products
            await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserHistoryProds);
            // User Favorite Products
			await _unitOfWork.ProductRepository.DeleteRange(prodcut.UserFavProds);

            // Delete related data
            await _unitOfWork.PriceHistoryRepository.DeleteRange(prodcut.PriceHistories);
            await _unitOfWork.ProductImageRepository.DeleteRange(prodcut.ProductImages);
            await _unitOfWork.ProductDetailRepository.DeleteRange(prodcut.ProductLinks.Select(pl => pl.ProductDetail));
			await _unitOfWork.ProductLinkRepository.DeleteRange(prodcut.ProductLinks);
			await _unitOfWork.ProductRepository.Delete(prodcut.Id);
            return Ok();
        }

        // Delete multiple products, products detials and related data
        [HttpDelete("bulk-delete")]
        public async Task<IActionResult> BulkDeleteProducts([FromBody] List<int> ids)
        {
            foreach (var id in ids)
            {
                var product = await _unitOfWork.ProductRepository.SelectById(id);
                if (product == null) continue;

                // User Alert Products
                await _unitOfWork.ProductRepository.DeleteRange(product.UserAlertProds);
                // User History Products
                await _unitOfWork.ProductRepository.DeleteRange(product.UserHistoryProds);
                // User Favorite Products
                await _unitOfWork.ProductRepository.DeleteRange(product.UserFavProds);

                // Delete related data
                await _unitOfWork.PriceHistoryRepository.DeleteRange(product.PriceHistories);
                await _unitOfWork.ProductImageRepository.DeleteRange(product.ProductImages);
                await _unitOfWork.ProductDetailRepository.DeleteRange(product.ProductLinks.Select(pl => pl.ProductDetail));
                await _unitOfWork.ProductLinkRepository.DeleteRange(product.ProductLinks);
                await _unitOfWork.ProductRepository.Delete(product.Id);
            }

            return Ok();
        }

		public enum SearchIn
		{
			Featured,
			MostPopular,
			MostViewed,
			All
		}

		public enum SortedBy
		{
			LowToHighPrice,
			HighToLowPrice,
			New,
			Featured,
			MostPopular,
			MostViewed,
			All
		}


		[HttpGet("search")]
		public async Task<IActionResult> SearchProduct(
	[FromQuery] string? searchValue = null,
	[FromQuery] int? categoryID = null,
	[FromQuery] int? subCatID = null,
	[FromQuery] List<int>? brandID = null,
	[FromQuery] int? minPrice = null,
	[FromQuery] int? maxPrice = null,
	[FromQuery] List<int>? domainID = null,
	[FromQuery] SearchIn searchIn = SearchIn.All
)
		{
			// Build the base query with necessary includes
			var query = _unitOfWork.ProductRepository
				.SelectAllProduct()
				.Include(p => p.SubCategory)
				.Include(p => p.Brand)
				.Include(p => p.PriceHistories)
				.Include(p => p.ProductImages)
				.Include(p => p.ProductLinks)
					.ThenInclude(pl => pl.ProductDetail)
				.AsQueryable();

			// Apply filters to the query
			if (!string.IsNullOrEmpty(searchValue))
			{
				query = query.Where(p =>
					p.Name_Global.Contains(searchValue) ||
					p.Description_Global.Contains(searchValue) ||
					p.Name_Local.Contains(searchValue) ||
					p.Description_Local.Contains(searchValue));
			}

			if (categoryID.HasValue)
			{
				query = query.Where(p => p.SubCategory.CategoryId == categoryID.Value);
			}

			if (subCatID.HasValue)
			{
				query = query.Where(p => p.SubCategory.Id == subCatID.Value);
			}

			if (brandID != null && brandID.Any())
			{
				query = query.Where(p => brandID.Contains(p.Brand.Id));
			}

			if (minPrice.HasValue && maxPrice.HasValue)
			{
				query = query.Where(p => p.ProductLinks.Any(pl => pl.ProductDetail.Price >= minPrice.Value && pl.ProductDetail.Price <= maxPrice.Value));
			}

			if (domainID != null && domainID.Any())
			{
				query = query.Where(p => p.ProductLinks.Any(pl => domainID.Contains(pl.Domain.Id)));
			}

			// Execute the query and project to DTOs
			var products = await query
				.Select(product => new SearchProductDTO
				{
					Product_Id = product.Id,
					Product_Name_Local = product.Name_Local,
					Product_Name_Global = product.Name_Global,
					Product_Description_Local = product.Description_Local,
					Product_Description_Global = product.Description_Global,
					brandPostDTO = new BrandPostDTO
					{
						Name_Local = product.Brand.Name_Local,
						Name_Global = product.Brand.Name_Global,
						Description_Local = product.Brand.Description_Local,
						Description_Global = product.Brand.Description_Global,
						Logo = product.Brand.Logo,
						CategoryId = product.Brand.Category.Id
					},
					subCategoryPostDTO = new SubCategoryPostDTO
					{
						Id = product.SubCategory.Id,
						Name_Local = product.SubCategory.Name_Local,
						Name_Global = product.SubCategory.Name_Global,
						CategoryId = product.SubCategory.Category.Id
					},
					productImageDTOs = product.ProductImages.Select(pi => new ProductImageDTO
					{
						Id = pi.Id,
						ProdId = pi.ProdId,
						Image = pi.Image
					}).ToList(),
					productLinkDTOs = product.ProductLinks.Select(pl => new ProudctLinkWithDetailsDTO
					{
						Link_Id = pl.Id,
						Link_DomainId = pl.Domain.Id,
						ProductLink = pl.ProductLink1,
						ProductDet_Name_Local = pl.ProductDetail.Name_Local,
						ProductDet_Name_Global = pl.ProductDetail.Name_Global,
						ProductDet_Description_Local = pl.ProductDetail.Description_Local,
						ProductDet_Description_Global = pl.ProductDetail.Description_Global,
						ProductDet_Price = pl.ProductDetail.Price,
						ProductDet_Rating = pl.ProductDetail.Rating,
						ProductDet_isAvailable = pl.ProductDetail.isAvailable
					}).ToList()
				})
				.ToListAsync();

			if (!products.Any()) return NotFound();

			return Ok(products);
		}




		//public async Task<IActionResult> Search(string? searchValue = null, Category? cat = null, SubCategory? subCat = null, Brand? brand = null, int? minPrice = null, int? maxPrice = null, List<Domain>? store = null, searchIn searchIn)
		//{

		//}
	}
}
