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


        public enum searchIn
        {
            featured,
            mostPopular,
            mostViewed,
            all,
        }
        public enum SortedBy
        {
            LowToHighPrice,
            HighToLowPrice,
            New,
            featured,
            mostPopular,
            mostViewed,
            all
        }

        // Search product 
        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct(
            [FromQuery] string? searchValue = null,
			[FromQuery] int? categoryID = null ,
			[FromQuery] int? subCatID = null,
			[FromQuery] List<int>? brandID = null,
			[FromQuery] int? minPrice = null,
		    [FromQuery] int? maxPrice = null,
			[FromQuery] List<int>? domainID = null
			//[FromQuery] searchIn searchIn = searchIn.all
			)
		{
            // Load products with related data using Eager Loading
            var products = await _unitOfWork.ProductRepository
                .SelectAllProduct()
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p=>p.PriceHistories)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pd => pd.ProductDetail)
                
                //.Include(p => p.SubCategory)
                //    .ThenInclude(SubCatCatId => SubCatCatId.Category.Id)
                //.Include(p => p.Brand)
                //.Include(p => p.ProductImages)
                //.Include(p => p.ProductLinks)
                //    .ThenInclude(pl => pl.Domain)
                //.Include(p => p.ProductLinks)
                //    .ThenInclude(pl => pl.ProductDetail)
                .ToListAsync();

            if (products == null || !products.Any()) return NotFound();

            //var combinedProductDetails = new List<CombinedProductDetailDTO>();
            List<SearchProductDTO> searchProductDTOs = new List<SearchProductDTO>();

            foreach (var product in products)
            {
                SearchProductDTO searchProduct = new SearchProductDTO()
                {
                    Product_Id = product.Id,
                    Product_Name_Local = product.Name_Local,
                    Product_Name_Global = product.Name_Global,
                    Product_Description_Local = product.Description_Local,
                    Product_Description_Global = product.Description_Global,
                    brandPostDTO = new BrandPostDTO()
                    {
                        // Id = product.Brand.Id,
                        Name_Local = product.Brand.Name_Local,
                        Name_Global = product.Brand.Name_Global,
                        Description_Local = product.Brand.Description_Local,
                        Description_Global = product.Brand.Description_Global,
                        Logo = product.Brand.Logo,
                        CategoryId = product.Brand.Category.Id

                    },
                    subCategoryPostDTO = new SubCategoryPostDTO()
                    {
                        Id = product.SubCategory.Id,
                        Name_Local = product.SubCategory.Name_Local,
                        Name_Global = product.SubCategory.Name_Global,
                        CategoryId = product.SubCategory.Category.Id
                    },
                    // Lists priceHistoryDTOs, productImageDTOs, productLinkDTOs
                    // public List<PriceHistoryDTO> priceHistoryDTOs { get; set; }
                    //priceHistoryDTOs = product.PriceHistories.Select(ph => new PriceHistoryDTO()
                    //{
                    //    Id = ph.Id,
                    //    Price = ph.Price,
                    //    Date = ph.Date
                    //}).ToList(),
                    productImageDTOs = product.ProductImages.Select(pi => new ProductImageDTO()
                    {
                        Id = pi.Id,
                        ProdId = pi.ProdId,
                        Image = pi.Image

                    }).ToList(),
                    productLinkDTOs = product.ProductLinks.Select(pl => new ProudctLinkWithDetailsDTO()
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



                    //SubCategoryName = product.SubCategory?.Name_Global,
                    //BrandName = product.Brand?.Name_Global,
                    //// Ensure non-empty lists are handled
                    //LastUpdated = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastUpdated) : DateTime.MinValue,
                    //LastScraped = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastScraped) : DateTime.MinValue,
                    //Images = product.ProductImages.Select(img => img.Image).ToList(),
                    //Links = product.ProductLinks.Select(link => new ProductLinkDTO2
                    //{
                    //    DomainName = link.Domain.Name_Global,
                    //    DomainLogo = link.Domain.Logo,
                    //    ProductLink = link.ProductLink1,
                    //    Price = link.ProductDetail?.Price ?? 0,
                    //    Rating = link.ProductDetail?.Rating
                    //}).ToList()
                };

                // adding the searchProduct to the list searchProductDTOs
                searchProductDTOs.Add(searchProduct);
            }

            //foreach (var product in products)
            //{
            //    var combinedProductDetail = new CombinedProductDetailDTO
            //    {
            //        ProductId = product.Id,
            //        ProductName_Local = product.Name_Local,
            //        ProductName_Global = product.Name_Global,
            //        ProductDescription_Local = product.Description_Local,
            //        ProductDescription_Global = product.Description_Global,
            //        SubCategoryName = product.SubCategory?.Name_Global,
            //        BrandName = product.Brand?.Name_Global,
            //        // Ensure non-empty lists are handled
            //        LastUpdated = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastUpdated) : DateTime.MinValue,
            //        LastScraped = product.ProductLinks.Any() ? product.ProductLinks.Max(link => link.LastScraped) : DateTime.MinValue,

            //        Images = product.ProductImages.Select(img => img.Image).ToList(),
            //        Links = product.ProductLinks.Select(link => new ProductLinkDTO2
            //        {
            //            DomainName = link.Domain.Name_Global,
            //            DomainLogo = link.Domain.Logo,
            //            ProductLink = link.ProductLink1,
            //            Price = link.ProductDetail?.Price ?? 0,
            //            Rating = link.ProductDetail?.Rating
            //        }).ToList()
            //    };

            //    combinedProductDetails.Add(combinedProductDetail);
            //}

            //// apply the category filter
            //if (categoryID != null)
            //{
            //    combinedProductDetails = combinedProductDetails.Where(p => p.sub == categoryID).ToList();
            //}

            // apply the search value in the product name, description
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchProductDTOs = searchProductDTOs.Where(p =>
                        p.Product_Name_Global.Contains(searchValue)
                    || p.Product_Description_Global.Contains(searchValue)
                    || p.Product_Name_Local.Contains(searchValue)
                    || p.Product_Description_Local.Contains(searchValue)).ToList();
            }

            // apply the search value in the product name, description
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchProductDTOs = searchProductDTOs.Where(p =>
                    p.Product_Name_Global.Contains(searchValue)
                    || p.Product_Description_Global.Contains(searchValue)
                    || p.Product_Name_Local.Contains(searchValue)
                    || p.Product_Description_Local.Contains(searchValue)).ToList();
            }

            // apply the category filter
            if (categoryID != null)
            {
                searchProductDTOs = searchProductDTOs.Where(p => p.subCategoryPostDTO.CategoryId == categoryID).ToList();
                // or using brand
                // searchProductDTOs = searchProductDTOs.Where(p => p.brandPostDTO.CategoryId == categoryID).ToList();
            }

            // apply the sub category filter
            if (subCatID != null)
            {
                searchProductDTOs = searchProductDTOs.Where(p => p.subCategoryPostDTO.Id == subCatID).ToList();
            }

            // apply the brand filter
            if (brandID != null)
            {
                searchProductDTOs = searchProductDTOs.Where(p => brandID.Contains(p.brandPostDTO.Id)).ToList();
            }

            // apply the price filter // between min and max price
            if (minPrice != null && maxPrice != null)
            {
                searchProductDTOs = searchProductDTOs.Where(p => p.productLinkDTOs.Any(pl => pl.ProductDet_Price >= minPrice && pl.ProductDet_Price <= maxPrice)).ToList();
            }

            // apply the domain filter
            if (domainID != null)
            {
                searchProductDTOs = searchProductDTOs.Where(p => p.productLinkDTOs.Any(pl => domainID.Contains(pl.Link_DomainId))).ToList();
            }

            return Ok(searchProductDTOs);

        }



        //public async Task<IActionResult> Search(string? searchValue = null, Category? cat = null, SubCategory? subCat = null, Brand? brand = null, int? minPrice = null, int? maxPrice = null, List<Domain>? store = null, searchIn searchIn)
        //{

        //}
    }
}
