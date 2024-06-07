﻿using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Repository;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly GenericRepository<Category> _category;
        private readonly DatabaseContext _db;

        public CategoryController(GenericRepository<Category> category, DatabaseContext db)
		{
			_category = category;
            _db = db;
        }

		// GET: api/Category
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			// using GenericRepository
			var categories = await _category.SelectAll();
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

        // GET: api/Category/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCategoriesIgnoringFilters()
        {
            // using GenericRepository
            var categories = await _category.SelectAllIgnoringFiltersAsync();
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
			var category = await _category.SelectById(id);
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
			await _category.Add(category);
			// return
			return Ok();
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
		{
			await _category.UpdateAsync(category);
			return Ok();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			await _category.Delete(id);
			return Ok();
		}

        // DELETE: api/Category/SoftDelete/5
        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteCategory(int id)
        {
            // Retrieve the entity using the repository
            var category = await _category.SelectById(id);
            if (category == null)
            {
                return NotFound();
            }

            // Use the EF Core API to set the shadow property
            _db.Entry(category).Property("IsDeleted").CurrentValue = true;

            // Save the changes
            await _db.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Category/5/Brands
        [HttpGet("{id}/Brands")]
		public async Task<IActionResult> GetBrandsByCategoryId(int id)
		{
			var category = await _category.SelectById(id);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category.Brands);
		}

	}
}
