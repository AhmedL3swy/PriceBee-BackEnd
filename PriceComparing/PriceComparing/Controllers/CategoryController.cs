using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Repository;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
        private readonly UnitOfWOrks _unitOfWork;
        private readonly DatabaseContext _db;

        public CategoryController(UnitOfWOrks unitOfWork, DatabaseContext db)
        {
            _unitOfWork = unitOfWork;
            _db = db;
        }

		// GET: api/Category
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
            // using GenericRepository
            var categories = await _unitOfWork.CategoryRepository.SelectAll();
            // Check
            if (categories == null) { return NotFound(); }
            // using DTO
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoriesDTO.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name_Local = category.Name_Local,
                    Name_Global = category.Name_Global,
                    //Brands = category.Brands,
                    //SubCategories = category.SubCategories
                });
                // using foreach for Brands and SubCategories
                foreach (var brand in category.Brands)
                {
                    categoriesDTO[categoriesDTO.Count - 1].Brands.Add(new BrandDTO()
                    {
                        id = brand.Id,
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
                        Name_global = subCategory.Name_Global,
                    });
                }
            }
            // return
            return Ok(categoriesDTO);
        }

        // GET: api/Category/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategoriesIgnoringFilters()
        {
            // using GenericRepository
            var categories = await _unitOfWork.CategoryRepository.SelectAllIgnoringFiltersAsync();
            // Check
            if (categories == null) { return NotFound(); }
            // using DTO
            List<CategoryDTO> categoriesDTO = new List<CategoryDTO>();
            foreach (var category in categories)
            {
                categoriesDTO.Add(new CategoryDTO()
                {
                    Id = category.Id,
                    Name_Local = category.Name_Local,
                    Name_Global = category.Name_Global,
                    // Brands = category.Brands,
                    // SubCategories = category.SubCategories
                });
            }
            // return
            return Ok(categoriesDTO);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			// using GenericRepository
			var category = await _unitOfWork.CategoryRepository.SelectById(id);
            // Check
            if (category == null) { return NotFound(); }
            // using DTO
            CategoryDTO categoryDTO = new CategoryDTO()
            {
                Id = category.Id,
                Name_Local = category.Name_Local,
                Name_Global = category.Name_Global,
                // Brands = category.Brands,
                // SubCategories = category.SubCategories
            };
            // return
            return Ok(categoryDTO);
        }


        // POST: api/Category
        [HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
		{
			// Check
			if (categoryDTO == null)
			{
				return BadRequest();
			}
			// using DTO
			Category category = new Category()
			{
				Name_Local = categoryDTO.Name_Local,
				Name_Global = categoryDTO.Name_Global,
				// Brands = category.Brands,
				// SubCategories = category.SubCategories
			};
			// using GenericRepository
			await _unitOfWork.CategoryRepository.Add(category);
            // return
            return Ok();
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
		{
            await _unitOfWork.CategoryRepository.UpdateAsync(category);
            return Ok();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
            await _unitOfWork.CategoryRepository.Delete(id);
            return Ok();
		}

        // DELETE: api/Category/SoftDelete/5
        //[HttpDelete("SoftDelete/{id}")]
        //public async Task<IActionResult> SoftDeleteCategory(int id)
        //{
        //    // Retrieve the entity using the repository
        //    var category = await _unitOfWork.CategoryRepository.SelectById(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
		
        //    // Use the EF Core API to set the shadow property
        //    _db.Entry(category).Property("IsDeleted").CurrentValue = true;

        //    // Save the changes
        //    await _db.SaveChangesAsync();

        //    return Ok();
        //}

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


    }
}
