using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;
using DTO;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly UnitOfWOrks _unitOfWork;
        public SubCategoryController(UnitOfWOrks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.SelectAll();
            if (subCategories == null) return NotFound();
            List<SubCategory> subCategoriesDTO = new List<SubCategory>();
            foreach (var subCategory in subCategories)
            {
                subCategoriesDTO.Add(new SubCategory()
                {
                    Id = subCategory.Id,
                    Name_Local = subCategory.Name_Local,
                    Name_Global = subCategory.Name_Global,
                    CategoryId = subCategory.CategoryId
                });
            }
            return Ok(subCategoriesDTO);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.SelectById(id);
            if (subCategory == null) return NotFound();
            SubCategory subCategoryDTO = new SubCategory()
            {
                Id = subCategory.Id,
                Name_Local = subCategory.Name_Local,
                Name_Global = subCategory.Name_Global,

                CategoryId = subCategory.CategoryId
            };
            return Ok(subCategoryDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory(SubCategoryPostDTO subCategoryPostDTO)
        {
            if (subCategoryPostDTO == null) return BadRequest();
            SubCategory subCategory = new SubCategory()
            {
                Name_Local = subCategoryPostDTO.Name_Local,
                Name_Global = subCategoryPostDTO.Name_Global,
                CategoryId = subCategoryPostDTO.CategoryId
            };
            await _unitOfWork.SubCategoryRepository.Add(subCategory);
            return Ok();

        }
            
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] SubCategoryPostDTO subCategoryPostDTO)
        {
            // Check // Get // Check
            if (subCategoryPostDTO == null) return BadRequest();
            var subCategoryToUpdate = await _unitOfWork.SubCategoryRepository.SelectById(id);
            if (subCategoryToUpdate == null) return NotFound();

            // Check // Get // Check (Category)
            if (subCategoryPostDTO.CategoryId == null) return BadRequest();
            var category = await _unitOfWork.CategoryRepository.SelectById(subCategoryPostDTO.CategoryId);
            if (category == null) return NotFound();

            // update 
            subCategoryToUpdate.Name_Local  = subCategoryPostDTO.Name_Local;
            subCategoryToUpdate.Name_Global = subCategoryPostDTO.Name_Global;
            subCategoryToUpdate.CategoryId  = subCategoryPostDTO.CategoryId;

            await _unitOfWork.SubCategoryRepository.UpdateAsync(subCategoryToUpdate);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.SelectById(id);
            if (subCategory == null) return NotFound();
            await _unitOfWork.SubCategoryRepository.Delete(id);
            return Ok();
        }
    }
}
