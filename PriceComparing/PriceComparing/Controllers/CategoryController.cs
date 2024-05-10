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
		private readonly DatabaseContext _db;

		public CategoryController(DatabaseContext db)
        {
            _db = db;
        }

		// GET: api/Category
		[HttpGet]
		public IActionResult GetAllCategories()
		{
			return Ok(_db.Categories);
		}

		// GET: api/Category/5
		[HttpGet("{id}")]
		public IActionResult GetCategoryById(int id)
		{
			var category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			return Ok(category);
		}


		// POST: api/Category
		[HttpPost]
		public IActionResult AddCategory([FromBody] Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
			return Ok();
		}

		// PUT: api/Category/5
		[HttpPut("{id}")]
		public IActionResult UpdateCategory(int id, [FromBody] Category category)
		{
			_db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			_db.SaveChanges();
			return Ok();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public IActionResult DeleteCategory(int id)
		{
			var category = _db.Categories.Find(id);
			if (category == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(category);
			_db.SaveChanges();
			return Ok();
		}

		// GET: api/Category/5/Brands

		//private readonly GenericRepository<Category> category;

		//public CategoryController(GenericRepository<Category> category)
		//      {
		//	this.category = category;
		//}
		//      // GET: api/Category
		//      [HttpGet]
		//public IActionResult GetAllCategories()
		//{
		//	//return Ok(category.selectall());
		//}
		//// GET: api/Category/5
		//[HttpGet("{id}")]
		//public IActionResult GetCategoryById(int id)
		//{
		//	return Ok(category.selectbyid(id));
		//}

		//// POST: api/Category
		//[HttpPost]
		//public IActionResult AddCategory([FromBody] Category category)
		//{
		//	this.category.add(category);
		//	this.category.save();
		//	return Ok();
		//}

		//// PUT: api/Category/5
		//[HttpPut("{id}")]
		//public IActionResult UpdateCategory(int id, [FromBody] Category category)
		//{
		//	this.category.update(category);
		//	this.category.save();
		//	return Ok();
		//}

		//// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//public IActionResult DeleteCategory(int id)
		//{
		//	this.category.delete(id);
		//	this.category.save();
		//	return Ok();
		//}

		//// GET: api/Category/5/Brands
		//[HttpGet("{id}/Brands")]
		//public IActionResult GetBrandsByCategoryId(int id)
		//{
		//	var category = this.category.selectbyid(id);
		//	if (category == null)
		//	{
		//		return NotFound();
		//	}
		//	return Ok(category.Brands);
	}


}

