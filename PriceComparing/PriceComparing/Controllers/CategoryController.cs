using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Repository;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly GenericRepository<Category> _category;

		public CategoryController(GenericRepository<Category> category)
		{
			_category = category;
		}

		// GET: api/Category
		[HttpGet]
		public async Task<IActionResult> GetAllCategories()
		{
			var categories = await _category.SelectAll();
			return Ok(categories);
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryById(int id)
		{
			var category = await _category.SelectById(id);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}

		// POST: api/Category
		[HttpPost]
		public async Task<IActionResult> AddCategory([FromBody] Category category)
		{
			if (category == null)
			{
				return BadRequest();
			}
			await _category.Add(category);
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
