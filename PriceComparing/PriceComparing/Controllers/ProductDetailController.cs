using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var product = await _unitOfWork.ProductRepository
                .SelectAllProduct()
                .Include(p => p.SubCategory)
                .Include(p => p.Brand)
                .Include(p => p.PriceHistories)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.Domain)
                .Include(p => p.ProductLinks)
                    .ThenInclude(pl => pl.ProductDetail)
                .Where(p => p.Id == id) // Filter by the given product ID
                .Select(product => new
                {
                    Product_Id = product.Id,
                    Product_Name_Local = product.Name_Local,
                    Product_Name_Global = product.Name_Global,
                    Product_Description_Local = product.Description_Local,
                    Product_Description_Global = product.Description_Global,
                    BrandLogo = product.Brand.Logo,
                    MinPrice = product.PriceHistories.Min(ph => ph.Price), // Calculate the minimum price
                    ProductImages = product.ProductImages.Select(pi => pi.Image).ToList(), // Select all product images
                    ProductDomains = product.ProductLinks.GroupBy(pl => pl.Domain).Select(g => new
                    {
                        DomainId = g.Key.Id,
                        DomainNameLocal = g.Key.Name_Local,
						DomainNameGlobal = g.Key.Name_Global,
                        DomainDescriptionLocal = g.Key.Description_Local,
						DomainDescriptionGlobal = g.Key.Description_Global,
                        LinkDetails = g.Select(pl => new
                        {
                            LinkId = pl.Id,
                            LinkName = pl.ProductLink1,
                            Price = pl.ProductDetail.Price,
                            Name = pl.ProductDetail.Name_Local // Assuming you want the local name of the product detail
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync(); // Since we're filtering by ID, we expect at most one result

            if (product == null) return NotFound();

            // Construct the DTO to include the minimum price, domain details, product images, and product domains
            var productDetailDTO = new
            {
                product.Product_Id,
                product.Product_Name_Local,
                product.Product_Name_Global,
                product.Product_Description_Local,
                product.Product_Description_Global,
                product.BrandLogo,
                MinPrice = product.MinPrice,
                ProductImages = product.ProductImages,
                ProductDomains = product.ProductDomains
            };

            return Ok(productDetailDTO);
        }




    }
}
