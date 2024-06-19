using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO; // Assuming DTOs for PaidProduct exist
using DataAccess.Models;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaidProductController : ControllerBase
    {
        private readonly UnitOfWOrks _unitOfWork;

        public PaidProductController(UnitOfWOrks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaidProducts()
        {
            var paidProducts = await _unitOfWork.PaidProductRepository.SelectAll();
            if (paidProducts == null) { return NotFound(); }
            var paidProductsDTO = paidProducts.Select(pp => new PaidProductDTO
            {
                Id = pp.Id,
                Name_Local = pp.Name_Local,
                Name_Global = pp.Name_Global,
                Description_Local = pp.Description_Local,
                Description_Global = pp.Description_Global,
                SubCategoryId = pp.SubCategoryId,
                BrandId = pp.BrandId,
                IsPaid = pp.IsPaid,
                Duration = pp.Duration,
                StartTime = pp.StartTime,
                EndTime = pp.EndTime,
                Budget = pp.Budget
            }).ToList();

            return Ok(paidProductsDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaidProductById(int id)
        {
            var paidProduct = await _unitOfWork.PaidProductRepository.SelectById(id);
            if (paidProduct == null) { return NotFound(); }
            var paidProductDTO = new PaidProductDTO
            {
                Id = paidProduct.Id,
                Name_Local = paidProduct.Name_Local,
                Name_Global = paidProduct.Name_Global,
                Description_Local = paidProduct.Description_Local,
                Description_Global = paidProduct.Description_Global,
                SubCategoryId = paidProduct.SubCategoryId,
                BrandId = paidProduct.BrandId,
                IsPaid = paidProduct.IsPaid,
                Duration = paidProduct.Duration,
                StartTime = paidProduct.StartTime,
                EndTime = paidProduct.EndTime,
                Budget = paidProduct.Budget
            };

            return Ok(paidProductDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddPaidProduct(PaidProductPostDTO paidProductDTO)
        {
            if (paidProductDTO == null) { return BadRequest(); }
            var paidProduct = new PaidProduct
            {
                // Assuming PaidProductPostDTO has similar structure to PaidProductDTO
                Name_Local = paidProductDTO.Name_Local,
                Name_Global = paidProductDTO.Name_Global,
                Description_Local = paidProductDTO.Description_Local,
                Description_Global = paidProductDTO.Description_Global,
                SubCategoryId = paidProductDTO.SubCategoryId,
                BrandId = paidProductDTO.BrandId,
                IsPaid = paidProductDTO.IsPaid,
                Duration = paidProductDTO.Duration,
                StartTime = paidProductDTO.StartTime,
                EndTime = paidProductDTO.EndTime,
                Budget = paidProductDTO.Budget
            };

            await _unitOfWork.PaidProductRepository.Add(paidProduct);
            return Ok(paidProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaidProduct(int id, [FromBody] PaidProductPostDTO paidProductDTO)
        {
            if (paidProductDTO == null) { return BadRequest(); }
            var paidProduct = await _unitOfWork.PaidProductRepository.SelectById(id);
            if (paidProduct == null) { return NotFound(); }

            // Update properties
            paidProduct.Name_Local = paidProductDTO.Name_Local;
            paidProduct.Name_Global = paidProductDTO.Name_Global;
            paidProduct.Description_Local = paidProductDTO.Description_Local;
            paidProduct.Description_Global = paidProductDTO.Description_Global;
          
            paidProduct.Duration = paidProductDTO.Duration;
            paidProduct.StartTime = paidProductDTO.StartTime;
            paidProduct.EndTime = paidProductDTO.EndTime;
            paidProduct.Budget = paidProductDTO.Budget;

            await _unitOfWork.PaidProductRepository.UpdateAsync(paidProduct);
            return Ok(paidProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaidProduct(int id)
        {
            var paidProduct = await _unitOfWork.PaidProductRepository.SelectById(id);
            if (paidProduct == null) { return NotFound(); }

            await _unitOfWork.PaidProductRepository.Delete(id);
            return Ok();
        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeletePaidProduct(int id)
        {
            var paidProduct = await _unitOfWork.PaidProductRepository.SelectById(id);
            if (paidProduct == null) { return NotFound(); }

            await _unitOfWork.PaidProductRepository.SoftDelete(id);
            return Ok();
        }
    }
}
