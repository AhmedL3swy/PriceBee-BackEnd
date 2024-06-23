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

        // get all ignore filters
        [HttpGet("all")]
        public async Task<IActionResult> GetAllSubCategoriesIgnoringFilters()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.SelectAllIgnoringFiltersAsync();
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

        // get all soft deleted
        [HttpGet("softDeleted")]
        public async Task<IActionResult> GetAllSoftDeletedSubCategories()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.SelectAllSoftDeletedAsync();
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

        // get by id ignore filters
        [HttpGet("ignore/{id}")]
        public async Task<IActionResult> GetSubCategoryByIdIgnoringFilters(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.SelectByIdIgnoringFiltersAsync(id);
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
        public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] SubCategory subCategory)
        {
            if (subCategory == null) return BadRequest();
            var subCategoryToUpdate = await _unitOfWork.SubCategoryRepository.SelectById(id);
            if (subCategoryToUpdate == null) return NotFound();
            subCategoryToUpdate.Name_Local = subCategory.Name_Local;
            subCategoryToUpdate.Name_Global = subCategory.Name_Global;
            subCategoryToUpdate.CategoryId = subCategory.CategoryId;
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

        // Soft Delete 
        [HttpDelete("soft/{id}")]
        public async Task<IActionResult> SoftDeleteSubCategory(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository.SelectById(id);
            if (subCategory == null) return NotFound();
            await _unitOfWork.SubCategoryRepository.SoftDelete(id);
            return Ok();
        }

        [HttpPost("restore/{id}")]
        public async Task<IActionResult> RestoreSubCategory(int id)
        {
            var subCategory = await _unitOfWork.SubCategoryRepository
                .SelectByIdIgnoringFiltersAsync(id); // Method to include soft-deleted items

            if (subCategory == null )// || !subCategory.IsDeleted)
            {
                return NotFound("SubCategory not found or already active.");
            }


            await _unitOfWork.SubCategoryRepository.RestoreAsync(subCategory);
            return Ok($"SubCategory with Id {subCategory.Id} has been restored.");
       //make one to get the count 
       [HttpGet("count")]
        public async Task<IActionResult> GetSubCategoryCount()
        {
            var subCategories = await _unitOfWork.SubCategoryRepository.SelectAll();
            if (subCategories == null) return NotFound();
            return Ok(subCategories.Count());
        }
    }
}
