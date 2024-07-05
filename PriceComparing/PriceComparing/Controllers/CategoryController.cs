using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using PriceComparing.Repository;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly UnitOfWOrks _unitOfWork;

        public CategoryController(UnitOfWOrks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 1- Get all active categories
        // GET: api/Category
        [HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
            var categories = await _unitOfWork.CategoryRepository.SelectAll();
            if (categories == null) { return NotFound(); }
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoriesDTO.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name_Local = category.Name_Local,
                    Name_Global = category.Name_Global,
                });
                foreach (var brand in category.Brands)
                {
                    categoriesDTO[categoriesDTO.Count - 1].Brands.Add(new BrandDTO()
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
                foreach (var subCategory in category.SubCategories)
                {
                    categoriesDTO[categoriesDTO.Count - 1].SubCategories.Add(new SubCategoryDTO()
                    {
                        id = subCategory.Id,
                        Name_Local = subCategory.Name_Local,
                        Name_Global = subCategory.Name_Global,
                        CategoryId = subCategory.CategoryId
                    });
                }
            }
            return Ok(categoriesDTO);
        }

        // 2- Get all categories ignoring filters
        // GET: api/Category/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategoriesIgnoringFilters()
        {
            var categories = await _unitOfWork.CategoryRepository.SelectAllIgnoringFiltersAsync();
            if (categories == null) { return NotFound(); }
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoriesDTO.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name_Local = category.Name_Local,
                    Name_Global = category.Name_Global,
                });
                foreach (var brand in category.Brands)
                {
                    categoriesDTO[categoriesDTO.Count - 1].Brands.Add(new BrandDTO()
                    {
                        Id = brand.Id,
                        Name_Local = brand.Name_Local,
                        Name_Global = brand.Name_Global,
                        Description_Local = brand.Description_Local,
                        Description_Global = brand.Description_Global,
                    });
                }

                foreach (var subCategory in category.SubCategories)
                {
                    categoriesDTO[categoriesDTO.Count - 1].SubCategories.Add(new SubCategoryDTO()
                    {
                        id = subCategory.Id,
                        Name_Local = subCategory.Name_Local,
                        Name_Global = subCategory.Name_Global,
                    });
                }
            }
            return Ok(categoriesDTO);
        }

        // 3- Get all soft deleted categories
        // GET: api/Category/SoftDeleted
        [HttpGet("SoftDeleted")]
        public async Task<IActionResult> GetAllSoftDeletedCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.SelectAllSoftDeletedAsync();
            if (categories == null) { return NotFound(); }
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoriesDTO.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name_Local = category.Name_Local,
                    Name_Global = category.Name_Global,
                });
                foreach (var brand in category.Brands)
                {
                    categoriesDTO[categoriesDTO.Count - 1].Brands.Add(new BrandDTO()
                    {
                        Id = brand.Id,
                        Name_Local = brand.Name_Local,
                        Name_Global = brand.Name_Global,
                        Description_Local = brand.Description_Local,
                        Description_Global = brand.Description_Global,
                    });
                }

                foreach (var subCategory in category.SubCategories)
                {
                    categoriesDTO[categoriesDTO.Count - 1].SubCategories.Add(new SubCategoryDTO()
                    {
                        id = subCategory.Id,
                        Name_Local = subCategory.Name_Local,
                        Name_Global = subCategory.Name_Global,
                    });
                }
            }
            return Ok(categoriesDTO);
        }

        // 4- Get category by id
        // GET: api/Category/5
        [HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var category = await _unitOfWork.CategoryRepository.SelectById(id);
            if (category == null) { return NotFound(); }
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name_Local = category.Name_Local,
                Name_Global = category.Name_Global,
                Brands = category.Brands.Select(brand => new BrandDTO
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global
                }).ToList(),
                SubCategories = category.SubCategories.Select(subCategory => new SubCategoryDTO
                {
                    id = subCategory.Id,
                    Name_Local = subCategory.Name_Local,
                    Name_Global = subCategory.Name_Global
                }).ToList()
            };
            return Ok(categoryDTO);
        }

        // 5- Get category by id ignoring filters
        [HttpGet("ignore/{id}")]
        public async Task<IActionResult> GetCategoryByIdIgnoringFilters(int id)
        {
            var category = await _unitOfWork.CategoryRepository.SelectByIdIgnoringFiltersAsync(id);
            if (category == null) { return NotFound(); }
            var categoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name_Local = category.Name_Local,
                Name_Global = category.Name_Global,
                Brands = category.Brands.Select(brand => new BrandDTO
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global
                }).ToList(),
                SubCategories = category.SubCategories.Select(subCategory => new SubCategoryDTO
                {
                    id = subCategory.Id,
                    Name_Local = subCategory.Name_Local,
                    Name_Global = subCategory.Name_Global
                }).ToList()
            };
            return Ok(categoryDTO);
        }

        // 6- Add category
        // POST: api/Category
        [HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] CategoryPostDTO categoryPostDTO)
		{
			if (categoryPostDTO == null) return BadRequest();
			Category category = new Category()
			{
				Name_Local = categoryPostDTO.Name_Local,
				Name_Global = categoryPostDTO.Name_Global,
			};
			await _unitOfWork.CategoryRepository.Add(category);
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Name_Local = category.Name_Local,
                Name_Global = category.Name_Global,
            };
            return Ok(categoryDTO);
        }

        // 7- Update category
        // PUT: api/Category/5
        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryPostDTO categoryPostDTO)
		{
            if (categoryPostDTO == null) return BadRequest();
            var category = await _unitOfWork.CategoryRepository.SelectByIdIgnoringFiltersAsync(id);
            if (category == null) return NotFound();
            category.Name_Local = categoryPostDTO.Name_Local;
            category.Name_Global = categoryPostDTO.Name_Global;

            await _unitOfWork.CategoryRepository.UpdateAsync(category);

            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Name_Local = category.Name_Local,
                Name_Global = category.Name_Global,
                Brands = category.Brands.Select(brand => new BrandDTO
                {
                    Id = brand.Id,
                    Name_Local = brand.Name_Local,
                    Name_Global = brand.Name_Global,
                    Description_Local = brand.Description_Local,
                    Description_Global = brand.Description_Global,
                    Logo = brand.Logo,
                    LogoUrl = brand.Logo,
                    CategoryId = brand.CategoryId
                }).ToList(),
                SubCategories = category.SubCategories.Select(subCategory => new SubCategoryDTO
                {
                    id = subCategory.Id,
                    Name_Local = subCategory.Name_Local,
                    Name_Global = subCategory.Name_Global,
                    CategoryId = subCategory.CategoryId
                }).ToList()
            };
            return Ok(categoryDTO);
		}

        // 8- Delete category
        // DELETE: api/Category/5
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
            var category = await _unitOfWork.CategoryRepository.SelectByIdIgnoringFiltersAsync(id);
            if (category == null) return NotFound();
            await _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.savechanges();

            return Ok();
		}

        // 9- Soft delete category
        // DELETE: api/Category/SoftDelete/5
        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.SelectByIdIgnoringFiltersAsync(id);
            if (category == null) return NotFound();
            await _unitOfWork.CategoryRepository.SoftDelete(id);
            _unitOfWork.savechanges();
            return Ok();
        }

        // 10- Restore category
        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.SelectByIdIgnoringFiltersAsync(id);
            if (category == null) return NotFound("Category not found or already restored");
            await _unitOfWork.CategoryRepository.Restore(category);
            _unitOfWork.savechanges();
            return Ok();
        }

        #region Category_Counters
        //make one to get the category count 
        [HttpGet("Count")]
        public async Task<IActionResult> GetCategoriesCount()
        {
            var categories = await _unitOfWork.CategoryRepository.SelectAll();
            if (categories == null) { return NotFound(); }
            return Ok(categories.Count());
        }

        [HttpGet("CategoriesBrandsCount")]
        public async Task<IActionResult> GetCategoriesBrandsCount()
        {
            // Assuming each brand has a Category property and each category has a Name property
            var categories = await _unitOfWork.CategoryRepository.SelectAll();
            if (categories == null) { return NotFound(); }

            var categoriesBrandsCountList = categories.Select(category => new CategoryBrandsCountDTO
            {
                CategoryName = category.Name_Global, // or Name_Global, depending on your requirement
                BrandsCount = category.Brands.Count
            }).ToList();

            return Ok(categoriesBrandsCountList);
        }
        #endregion

        // GET: api/Category/5/Brands
        [HttpGet("{id}/Brands")]
        public async Task<IActionResult> GetBrandsByCategoryId(int id)
        {
            var category = await _unitOfWork.CategoryRepository.SelectById(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category.Brands);
        }
    }
}
