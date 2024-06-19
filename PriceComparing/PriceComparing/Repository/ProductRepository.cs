using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using DTO;
namespace PriceComparing.Repository
{
    public class ProductRepository
    {
        private readonly DatabaseContext _db;
        public ProductRepository(DatabaseContext db)
        {
            _db = db;
        }
        public async Task<int> Add(ProductPostDTO productDTO)
        {
            Product product = new Product()
            {
                Name_Local = productDTO.Name_Global,
                Name_Global = productDTO.Name_Local,
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
            }

            // Save changes after all ProductDetails have been added
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
                    id = b.Id,
                    Name_Local = b.Name_Local,
                    Logo = b.Logo,
                    CategiryId = b.CategoryId,
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
                    Name_global = sc.Name_Global,
                    Categoryid = sc.CategoryId,
                   
                })
                .ToListAsync();
            return subCategories;
        }
        



    }
}
