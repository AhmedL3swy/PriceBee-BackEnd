using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DTO;
using PriceComparing.Services;
using System.Security.Policy;
namespace PriceComparing.Repository
{
    public class ProductRepository
    {
        private readonly DatabaseContext _db;
        private readonly ScrapingService _scrapingService;
        public ProductRepository(DatabaseContext db, ScrapingService scrapingService)
        {
            _db = db;
            _scrapingService = scrapingService;
        }
        public async Task<int> Add(ProductPostDTO productDTO)
        {
            Product product = new Product()
            {
                Name_Local = productDTO.Name_Local,
                Name_Global = productDTO.Name_Global,
                Description_Local = productDTO.Description_Local,
                Description_Global = productDTO.Description_Global,
                SubCategoryId = productDTO.SubCategoryId,
                BrandId = productDTO.BrandId,
            };
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product.Id;
        }
        public async Task AppendProductDetails(int productId, List<ProductDetailPostDTO> ProductsDetails)
        {
            foreach (var product in ProductsDetails)
            {
                // Create a new ProductLink object and set its properties
                ProductLink productLink = new ProductLink()
                {
                    ProdId = productId,
                    DomainId = product.DomainId,
                    ProductLink1 = product.ProductLink1,
                    Status = product.Status,
                    LastUpdated = product.LastUpdated,
                    LastScraped = product.LastScraped
                };

                // Add the ProductLink to the context and save changes to get the generated Id
                await _db.ProductLinks.AddAsync(productLink);
                await _db.SaveChangesAsync();

                // Use the generated Id for the new ProductDetail
                ProductDetail productDetail = new ProductDetail()
                {
                    Id = productLink.Id, // Use the generated Id
                    Name_Local = product.Name_Local,
                    Name_Global = product.Name_Global,
                    Description_Local = product.Description_Local,
                    Description_Global = product.Description_Global,
                    Price = product.Price,
                    Rating = product.Rating,
                    isAvailable = product.isAvailable,
                    Brand = product.Brand
                };

                // Add the ProductDetail to the context
                await _db.ProductDetails.AddAsync(productDetail);
                // add Price History
                 await AppendProductPriceHistory(productLink.Id, product.Price);
            }

            // Save changes after all ProductDetails have been added
            await _db.SaveChangesAsync();
        }
        // Append Product Price History
        public async Task AppendProductPriceHistory(int productId, decimal price)
        {
            PriceHistory priceHistory = new PriceHistory()
            {
                ProdId = productId,
                Price = price,
                Date = DateTime.Now
            };
            await _db.PriceHistories.AddAsync(priceHistory);
            await _db.SaveChangesAsync();
        }
        public async Task AppendProductImages(int productId, List<string> images)
        {
            foreach (var image in images)
            {
                ProductImage productImage = new ProductImage()
                {
                    ProdId = productId,
                    Image = image
                };
                await _db.ProductImages.AddAsync(productImage);
            }
            await _db.SaveChangesAsync();
        }
        // Get Brand By Category ID
        public async Task<List<BrandDTO>> GetBrandsByCategoryId(int categoryId)
        {
            List<BrandDTO> brands = await _db.Brands
                .Where(b => b.CategoryId==categoryId)
                .Select(b => new BrandDTO()
                {
                    Id = b.Id,
                    Name_Local = b.Name_Local,
                    Logo = b.Logo,
					LogoUrl = b.LogoUrl,
					CategoryId = b.CategoryId,
                    Description_Local = b.Description_Local,
                    Description_Global = b.Description_Global,
                    Name_Global = b.Name_Global
        
                })
                .ToListAsync();
            return brands;
        }
        // Get SubCategory By Category ID
        public async Task<List<SubCategoryDTO>> GetSubCategoriesByCategoryId(int categoryId)
        {
            List<SubCategoryDTO> subCategories = await _db.SubCategories
                .Where(sc => sc.CategoryId == categoryId)
                .Select(sc => new SubCategoryDTO()
                {
                    id = sc.Id,
                    Name_Local = sc.Name_Local,
                    Name_Global = sc.Name_Global,
                    CategoryId = sc.CategoryId,
                   
                })
                .ToListAsync();
            return subCategories;
        }

        public async Task UpdateProductPrice()
        {
            
            // Get ProductLink which has the least Scraped date
            var productLink = _db.ProductLinks.OrderBy(p => p.LastScraped).FirstOrDefault();
            if (productLink == null)
            {
                return;
            }
            productLink.LastScraped = DateTime.Now;

            // Get the ProductDetail of the ProductLink
            var productDetail = _db.ProductDetails.Find(productLink.Id);

            try
            {
                // get the latest price from the scraping service
                ScrapingDTO result = await _scrapingService.Get("SingleScrape", productLink.ProductLink1);
                // if the price is different from the current price update the price and Set LastUpdated to the current date
                if (productDetail.Price != result.price)
                {
                    productDetail.Price = result.price;
                    productLink.LastUpdated = DateTime.Now;
                    // Add the new price to the PriceHistory
                    await AppendProductPriceHistory(productLink.Id, result.price);
                }
                
            }
            // if Exception Set Status to false
            catch (Exception)
            {
                productLink.Status = "Failed";
            }
            finally
            {
                await _db.SaveChangesAsync();

            }

        }


    }
}
