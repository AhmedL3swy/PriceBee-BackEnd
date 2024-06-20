using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Repository;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly UnitOfWOrks _unitOfWork;
        public BrandController(UnitOfWOrks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _unitOfWork.BrandRepository.SelectAll();
            if (brands == null) return NotFound();
            List<Brand> brandsDTO = new List<Brand>();
            foreach (var brand in brands)
            {
                brandsDTO.Add(new Brand()
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global,
                    Logo = brand.Logo
                });
            }
            return Ok(brandsDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            Brand brandDTO = new Brand()
            {
                Id = brand.Id,
                Name_Local = brand.Name_Local,
                Name_Global = brand.Name_Global,
                Description_Local = brand.Description_Local,
                Description_Global = brand.Description_Global,
                Logo = brand.Logo
            };
            return Ok(brandDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandPostDTO brandpostDTo)
        {
            if (brandpostDTo == null) return BadRequest();
            Brand brand = new Brand()
            {
                Name_Local = brandpostDTo.Name_Local,
                Name_Global = brandpostDTo.Name_Global,
                Description_Local = brandpostDTo.Description_Local,
                Description_Global = brandpostDTo.Description_Global,
                Logo = brandpostDTo.Logo
               
            };
            await _unitOfWork.BrandRepository.Add(brand);
           
            return Ok(brand);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id,[FromBody] BrandPostDTO brandDTO)
        {
            if (brandDTO == null) return BadRequest();
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            brand.Name_Local = brandDTO.Name_Local;
            brand.Name_Global = brandDTO.Name_Global;
            brand.Description_Local = brandDTO.Description_Local;
            brand.Description_Global = brandDTO.Description_Global;
            brand.Logo = brandDTO.Logo;
            await _unitOfWork.BrandRepository.UpdateAsync(brand);
            
            return Ok(brandDTO);
            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            await _unitOfWork.BrandRepository.Delete(id);
            _unitOfWork.savechanges();
            return Ok();

        }

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            await _unitOfWork.BrandRepository.SoftDelete(id);
            _unitOfWork.savechanges();
            return Ok();
        }

        
      

    }
}
