using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddProductController : ControllerBase
    {
        UnitOfWOrks _UnitOfWork;

        public AddProductController(UnitOfWOrks unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO productDTO)
        {
           
            // Add The Product
            int productID = await _UnitOfWork.ProductRepo.Add(productDTO.ProductPostDTO);
            // Append Product Details
            await _UnitOfWork.ProductRepo.AppendProductDetails(productID, productDTO.ProductDetailDTO);
            // Append Product Images
            await _UnitOfWork.ProductRepo.AppendProductImages(productID, productDTO.ProductDetailDTO[0].Images);
               
             _UnitOfWork.savechanges();
            return Ok(productID);
        }
        // get Brands By Category
        [HttpGet]
        [Route("GetBrandsByCategory/{id}")]
        public async Task<IActionResult> GetBrandsByCategory(int id)
        {
            var brands = await _UnitOfWork.ProductRepo.GetBrandsByCategoryId(id);
            if (brands == null) return NotFound();
            return Ok(brands);
        }
        // get SubCategories By Category
        [HttpGet]
        [Route("GetSubCategoriesByCategory/{id}")]
        public async Task<IActionResult> GetSubCategoriesByCategory(int id)
        {
            var subCategories = await _UnitOfWork.ProductRepo.GetSubCategoriesByCategoryId(id);
            if (subCategories == null) return NotFound();
            return Ok(subCategories);
        }
    }
}
