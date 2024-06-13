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
    }
}
