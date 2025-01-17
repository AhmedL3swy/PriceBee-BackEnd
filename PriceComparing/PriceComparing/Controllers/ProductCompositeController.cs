﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

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
                .SelectAllSync()
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

		//create dummy api to test ci cd in publish with route dummyapi 
		[HttpGet("dummyapi")]
		public async Task<IActionResult> GetDummyApi()
		{
			return Ok("This is a dummy api to test CI/CD in publish");
		}
		[HttpGet("dummyapi2")]
		public async Task<IActionResult> GetDummyApii()
		{
			return Ok("This is a dummy api to test CI/CD in publish");
		}
		[HttpGet("dummyapi3")]
		public async Task<IActionResult> GetDummyApiii()
		{
			return Ok("This is a dummy api to test CI/CD in publish");
		}

		//for product details component
		[HttpGet("{id}")]
        public async Task<IActionResult> GetCombinedProductDetailsByID(int id)
        {
            // Filter to load the specific product with related data using Eager Loading
            var product = await _unitOfWork.ProductRepository
                .SelectAllSync()
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


        // for featured component
        [HttpGet("featured")]
        
        public async Task<IActionResult> GetCombinedProductDetailsForFeatured(int page = 1)
        {
            const int pageSize = 12; // Number of products per page
            var skip = (page - 1) * pageSize; // Calculate the number of products to skip

            // Load products with active sponsorships using Eager Loading
            var currentDate = DateTime.Now;
            var featuredProducts = await _unitOfWork.ProductRepository
                .SelectAllSync()
                .Where(p => p.ProductLinks.Any(pl => pl.ProductDetail.ProductSponsoreds.Any(ps => ps.StartDate <= currentDate && currentDate <= ps.StartDate.AddDays(ps.Duration))))
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.Domain)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                .OrderBy(p => p.Id) 
                .Skip(skip) 
                .Take(pageSize) 
                .ToListAsync();

            if (featuredProducts == null || !featuredProducts.Any()) return NotFound();

            var combinedProductDetailsList = new List<ProductCompositeForHomeDTO>();

            // Convert each product to a DTO
            foreach (var product in featuredProducts)
            {
                var combinedProductDetail = CreateCombinedProductDetailDTO(product); // Assuming CreateCombinedProductDetailDTO properly converts a Product to ProductCompositeForHomeDTO
                combinedProductDetailsList.Add(combinedProductDetail);
            }

            return Ok(combinedProductDetailsList);
        }

        //for most popular component
        [HttpGet("MOstPopular")]
        public async Task<IActionResult> GetCombinedProductDetailsForMostPopular(int page = 1)
        {
            const int pageSize = 10; // Define how many products per page you want
            var skipAmount = (page - 1) * pageSize; // Calculate the number of products to skip

           
            var mostPopularProductsQuery = _unitOfWork.ProductRepository
                .SelectAllSync()
                .OrderByDescending(p => p.NumberOfClicks) // Assuming NumberOfClicks indicates popularity
                .Skip(skipAmount)
                .Take(pageSize);

            var mostPopularProducts = await mostPopularProductsQuery
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.Domain)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                .ToListAsync();

            if (!mostPopularProducts.Any())
            {
                return NotFound("No popular products found.");
            }

            // Convert the products to your DTO
            var productDTOs = mostPopularProducts.Select(p => CreateCombinedProductDetailDTO(p)).ToList();

            return Ok(productDTOs);
        }

        // Assuming CreateCombinedProductDetailDTO is a method that converts a Product to a DTO
        




        //for home component 

        [HttpGet("home")]
        public async Task<IActionResult> GetCombinedProductDetailsForHome(int page = 1)
        {

            int pageSize = 12; // Number of products per page
                               // Calculate the number of products to skip
            int skip = (page - 1) * pageSize;

            // Load products with related data using Eager Loading
            var products = await _unitOfWork.ProductRepository
                .SelectAllSync()
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.Domain)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                    .OrderBy(p => p.Id) // Ensure there is an order, so pagination works correctly
            .Skip(skip) // Skip the previous pages' products
            .Take(pageSize) // Take the next set of products

                .ToListAsync();

            if (products == null || !products.Any()) return NotFound();

            var combinedProductDetailsList = new List<ProductCompositeForHomeDTO>();

            // Refactored to use the CreateCombinedProductDetailDTO method
            foreach (var product in products)
            {
                var combinedProductDetail = CreateCombinedProductDetailDTO(product);
                combinedProductDetailsList.Add(combinedProductDetail);
            }

            return Ok(combinedProductDetailsList);
        }

        // Assuming CreateCombinedProductDetailDTO is defined within the same controller class
        private ProductCompositeForHomeDTO CreateCombinedProductDetailDTO(Product product)
        {
            var minPriceLink = product.ProductLinks
                .Where(pl => pl.ProductDetail != null)
                .OrderBy(pl => pl.ProductDetail.Price)
                .FirstOrDefault();

            return new ProductCompositeForHomeDTO
            {
                ProductId = product.Id,
                ProductName_Local = product.Name_Local,
                ProductName_Global = product.Name_Global,
                Images = product.ProductImages.Select(img => img.Image).ToList().FirstOrDefault(),
                Links = product.ProductLinks.Select(link => new ProductLinkDTOForHOme
                {
                    DomainName = link.Domain.Name_Global,
                    DomainLogo = link.Domain.Logo,
                    ProductLink = link.ProductLink1,
                    Price = link.ProductDetail?.Price ?? 0
                   
                }).ToList(),
                MinPrice = minPriceLink?.ProductDetail?.Price ?? 0,
                MinPriceDomainLogo = minPriceLink?.Domain?.Logo,
                MinPriceBrandName = minPriceLink?.ProductDetail?.Brand, // Ensure this matches your model
                DomainCount = product.ProductLinks.Select(link => link.Domain.Id).Distinct().Count() // Count of unique domains
            };
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

        //public enum SearchIn
        //{
        //	Featured,
        //	MostPopular,
        //	MostViewed,
        //	All
        //}

        public enum SortedBy
        {
            [EnumMember(Value = "None")]
            None,
            [EnumMember(Value = "HightToLowPrice")]
            HightToLowPrice,

            [EnumMember(Value = "LowToHighPrice")]
            LowToHighPrice,

            [EnumMember(Value = "MostViewed")]
            MostViewed,

            [EnumMember(Value = "MostPopular")]
            MostPopular,

            [EnumMember(Value = "MostFavorite")]
            MostFavorite,

            [EnumMember(Value = "Newest")]
            Newest,

            [EnumMember(Value = "Oldest")]
            Oldest
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
            [FromQuery] Boolean isFeatured = false,
            [FromQuery] SortedBy sortedBy = SortedBy.None,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            // Build the base query with necessary includes
            var query = _unitOfWork.ProductRepository
                .SelectAllSync()
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.PriceHistories)
                .Include(p => p.ProductImages)
                .Include(p => p.UserFavProds)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                    .ThenInclude(pd => pd.ProductSponsoreds)
                .Include(p=>p.ProductLinks)
                    .ThenInclude(d=>d.Domain)
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

            if (isFeatured)
            {
                query = query.Where(p => p.ProductLinks.Any(pl => pl.ProductDetail.ProductSponsoreds.Any(ps => ps.StartDate <= DateTime.Now && ps.StartDate.AddDays(ps.Duration) >= DateTime.Now)));
            }

            // Execute the query and project to DTOs
            var products = await query
                .Select(product => new
                {
                    product.Id,
                    product.Name_Local,
                    product.Name_Global,
                    product.Description_Local,
                    product.Description_Global,
                    product.NumberOfClicks,
                    UserFavProds = product.UserFavProds.Count,
                    Brand = new
                    {
                        product.Brand.Name_Local,
                        product.Brand.Name_Global,
                        product.Brand.Description_Local,
                        product.Brand.Description_Global,
                        product.Brand.Logo,
                        CategoryId = product.Brand.Category.Id
                    },
                    SubCategory = new
                    {
                        product.SubCategory.Id,
                        product.SubCategory.Name_Local,
                        product.SubCategory.Name_Global,
                        CategoryId = product.SubCategory.Category.Id
                    },
                    ProductImages = product.ProductImages.Select(pi => new
                    {
                        pi.Id,
                        pi.ProdId,
                        pi.Image
                    }).ToList(),
                    ProductLinks = product.ProductLinks.Select(pl => new
                    {
                        pl.Id,
                        DomainId = pl.Domain.Id,
                        pl.ProductLink1,
                        ProductDetail = new
                        {
                            pl.ProductDetail.Name_Local,
                            pl.ProductDetail.Name_Global,
                            pl.ProductDetail.Description_Local,
                            pl.ProductDetail.Description_Global,
                            pl.ProductDetail.Price,
                            pl.ProductDetail.Rating,
                            pl.ProductDetail.isAvailable,
                            pl.LastUpdated,
                            pl.Domain,
                            ProductSponsoreds = pl.ProductDetail.ProductSponsoreds.Select(ps => new
                            {
                                ps.Id,
                                ps.Cost,
                                ps.StartDate,
                                ps.Duration,
                                ps.ProdDetId
                            }).ToList()
                        }
                    }).ToList()
                })
                .ToListAsync();

            // Perform the in-memory grouping and filtering to get the minimum price for each domain
            var result = products.Select(product => new SearchProductDTO
            {
                Product_Id = product.Id,
                Product_Name_Local = product.Name_Local,
                Product_Name_Global = product.Name_Global,
                Product_Description_Local = product.Description_Local,
                Product_Description_Global = product.Description_Global,
                Product_NumberOfClicks = product.NumberOfClicks,
                averageRating = product.ProductLinks.Any() ? product.ProductLinks.Average(a => a.ProductDetail.Rating) : 0,
                numberOfFavorites = product.UserFavProds,
                mostMinimumPrice = product.ProductLinks.Any() ? product.ProductLinks.Min(pl => pl.ProductDetail.Price) : 0,
                brandPostDTO = new BrandPostDTO
                {
                    Name_Local = product.Brand.Name_Local,
                    Name_Global = product.Brand.Name_Global,
                    Description_Local = product.Brand.Description_Local,
                    Description_Global = product.Brand.Description_Global,
                    Logo = product.Brand.Logo,
                    CategoryId = product.Brand.CategoryId
                },
                subCategoryPostDTO = new SubCategoryPostDTO
                {
                    Id = product.SubCategory.Id,
                    Name_Local = product.SubCategory.Name_Local,
                    Name_Global = product.SubCategory.Name_Global,
                    CategoryId = product.SubCategory.CategoryId
                },
                productImageDTOs = product.ProductImages.Select(pi => new ProductImageDTO
                {
                    Id = pi.Id,
                    ProdId = pi.ProdId,
                    Image = pi.Image
                }).ToList(),
                productLinkDTOs = product.ProductLinks
                    .GroupBy(pl => pl.DomainId)
                    .Select(g => g.OrderBy(pl => pl.ProductDetail.Price).First())
                    .Select(pl => new ProudctLinkWithDetailsDTO
                    {
                        Link_Id = pl.Id,
                        ProductLink = pl.ProductLink1,
                        ProductDet_Name_Local = pl.ProductDetail.Name_Local,
                        ProductDet_Name_Global = pl.ProductDetail.Name_Global,
                        ProductDet_Description_Local = pl.ProductDetail.Description_Local,
                        ProductDet_Description_Global = pl.ProductDetail.Description_Global,
                        ProductDet_Price = pl.ProductDetail.Price,
                        ProductDet_Rating = pl.ProductDetail.Rating,
                        ProductDet_isAvailable = pl.ProductDetail.isAvailable,
                        LastUpdated = pl.ProductDetail.LastUpdated,
                        // Domain Required Data
                        domainDTO = new DomainDTO
                        {
                            Id = pl.ProductDetail.Domain.Id,
                            Name_Local = pl.ProductDetail.Domain.Name_Local,
                            Name_Global = pl.ProductDetail.Domain.Name_Global,
                            Description_Local = pl.ProductDetail.Domain.Description_Local,
                            Description_Global = pl.ProductDetail.Domain.Description_Global,
                            Url = pl.ProductDetail.Domain.Url,
                            Logo = pl.ProductDetail.Domain.Logo
                        },
                        // sponsorship data
                        productSponsoredDTOs = pl.ProductDetail.ProductSponsoreds.Select(ps => new ProductSponsoredDTO
                        {
                            Id = ps.Id,
                            Cost = ps.Cost,
                            StartDate = ps.StartDate,
                            Duration = ps.Duration,
                            ProdDetId = ps.ProdDetId
                        }).ToList()
                    }).ToList()
            }).ToList();

            // Sorting the products
            switch (sortedBy)
            {
                case SortedBy.HightToLowPrice:
                    // sorting by the most minimum price
                    result = result.OrderByDescending(p => p.mostMinimumPrice).ToList();
                    break;
                case SortedBy.LowToHighPrice:
                    // sorting by the most minimum price
                    result = result.OrderBy(p => p.mostMinimumPrice).ToList();
                    break;
                case SortedBy.MostViewed:
                    result = result.OrderByDescending(p => p.Product_NumberOfClicks).ToList();
                    break;
                case SortedBy.MostPopular:
                    result = result.OrderByDescending(p => p.averageRating).ToList();
                    break;
                case SortedBy.MostFavorite:
                    result = result.OrderByDescending(p => p.numberOfFavorites).ToList();
                    break;
                case SortedBy.Newest:
                    result = result.OrderByDescending(p => p.AddedDate).ToList();
                    break;
                case SortedBy.Oldest:
                    result = result.OrderBy(p => p.AddedDate).ToList();
                    break;
                case SortedBy.None:
                    break;
            }

            if (!result.Any()) return NotFound();

            // get pageNumber, numberOfProducts
            var resultPaginated = result
                                    .Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return Ok(resultPaginated);
        }




        //public async Task<IActionResult> Search(string? searchValue = null, Category? cat = null, SubCategory? subCat = null, Brand? brand = null, int? minPrice = null, int? maxPrice = null, List<Domain>? store = null, searchIn searchIn)
        //{

        //}
    }
}
