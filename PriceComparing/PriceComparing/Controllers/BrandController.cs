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

        // 1- Get all active brands
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            var brands = await _unitOfWork.BrandRepository.SelectAll();
            if (brands == null) return NotFound();
            List<BrandDTO> brandsDTO = new List<BrandDTO>();
            foreach (var brand in brands)
            {
                brandsDTO.Add(new BrandDTO()
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global,
                    Logo = brand.Logo,
					LogoUrl = brand.LogoUrl,
					CategoryId = brand.CategoryId
				});
            }
            return Ok(brandsDTO);
        }

        // 2- Get all brands ignoring filters
        [HttpGet("all")]
        public async Task<IActionResult> GetAllBrandsIgnoringFilters()
        {
            var brands = await _unitOfWork.BrandRepository.SelectAllIgnoringFiltersAsync();
            if (brands == null) return NotFound();
            List<BrandDTO> brandsDTO = new List<BrandDTO>();
            foreach (var brand in brands)
            {
                brandsDTO.Add(new BrandDTO()
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global,
                    Logo = brand.Logo,
					LogoUrl = brand.LogoUrl,
					CategoryId = brand.CategoryId
				});
            }
            return Ok(brandsDTO);
        }

        // 3- Get all soft deleted brands
        [HttpGet("softdeleted")]
        public async Task<IActionResult> GetAllSoftDeletedBrands()
        {
            var brands = await _unitOfWork.BrandRepository.SelectAllSoftDeletedAsync();
            if (brands == null) return NotFound();
            List<BrandDTO> brandsDTO = new List<BrandDTO>();
            foreach (var brand in brands)
            {
                brandsDTO.Add(new BrandDTO()
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global,
                    Logo = brand.Logo,
					LogoUrl = brand.LogoUrl,
					CategoryId = brand.CategoryId
				});
            }
            return Ok(brandsDTO);
        }

        // 4- Get brand by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrandById(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            BrandDTO brandDTO = new BrandDTO()
            {
                Id = brand.Id,
                Name_Local = brand.Name_Local,
                Name_Global = brand.Name_Global,
                Description_Local = brand.Description_Local,
                Description_Global = brand.Description_Global,
                Logo = brand.Logo,
				LogoUrl = brand.LogoUrl,
			};
            return Ok(brandDTO);
        }

        // 5- Get brand by id ignoring filters
        [HttpGet("ignore/{id}")]
        public async Task<IActionResult> GetBrandByIdIgnoringFilters(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectByIdIgnoringFiltersAsync(id);
            if (brand == null) return NotFound();
            BrandDTO brandDTO = new BrandDTO()
            {
                Id = brand.Id,
                Name_Local = brand.Name_Local,
                Name_Global = brand.Name_Global,
                Description_Local = brand.Description_Local,
                Description_Global = brand.Description_Global,
                Logo = brand.Logo,
				LogoUrl = brand.LogoUrl
			};
            return Ok(brandDTO);
        }

        // 6- Add brand
        [HttpPost]
        public async Task<IActionResult> AddBrand(BrandPostDTO brandPostDTO)
        {
            if (brandPostDTO == null) return BadRequest();
            Brand brand = new Brand()
            {
                Name_Local = brandPostDTO.Name_Local,
                Name_Global = brandPostDTO.Name_Global,
                Description_Local = brandPostDTO.Description_Local,
                Description_Global = brandPostDTO.Description_Global,
                Logo = brandPostDTO.Logo,
                LogoUrl = brandPostDTO.LogoUrl,
				CategoryId = brandPostDTO.CategoryId
            };
            await _unitOfWork.BrandRepository.Add(brand);

            BrandDTO brandDTO = new BrandDTO()
            {
                Id = brand.Id,
                Name_Local = brand.Name_Local,
                Name_Global = brand.Name_Global,
                Description_Local = brand.Description_Local,
                Description_Global = brand.Description_Global,
                Logo = brand.Logo,
				LogoUrl = brand.LogoUrl
			};


            return Ok(brandDTO);
        }

        // 7- Update brand
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand(int id,[FromBody] BrandPostDTO brandPostDTO)
        {
            if (brandPostDTO == null) return BadRequest();
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            brand.Name_Local = brandPostDTO.Name_Local;
            brand.Name_Global = brandPostDTO.Name_Global;
            brand.Description_Local = brandPostDTO.Description_Local;
            brand.Description_Global = brandPostDTO.Description_Global;
            brand.Logo = brandPostDTO.Logo;
			brand.LogoUrl = brandPostDTO.LogoUrl;
			brand.CategoryId = brandPostDTO.CategoryId;

            await _unitOfWork.BrandRepository.UpdateAsync(brand);

            BrandDTO brandDTO = new BrandDTO()
            {
                Id = brand.Id,
                Name_Local = brand.Name_Local,
                Name_Global = brand.Name_Global,
                Description_Local = brand.Description_Local,
                Description_Global = brand.Description_Global,
                Logo = brand.Logo,
				LogoUrl = brand.LogoUrl
			};


            return Ok(brandDTO);
            
        }

        // 8- Delete brand
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            await _unitOfWork.BrandRepository.Delete(id);
            _unitOfWork.savechanges();

            return Ok();
        }

        // 9- Soft delete brand
        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> SoftDeleteBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectById(id);
            if (brand == null) return NotFound();
            await _unitOfWork.BrandRepository.SoftDelete(id);
            _unitOfWork.savechanges();
            return Ok();
        }

        // 10- Restore brand
        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreBrand(int id)
        {
            var brand = await _unitOfWork.BrandRepository.SelectByIdIgnoringFiltersAsync(id);
            if (brand == null) return NotFound();
            await _unitOfWork.BrandRepository.RestoreAsync(brand);
            _unitOfWork.savechanges();
            return Ok();
        }
    }
}
