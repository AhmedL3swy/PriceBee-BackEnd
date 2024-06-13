using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DTO
{
	public class CategoryPostDTO
	{
		public required string Name_Local { get; set; }
		public required string Name_Global { get; set; }
		public virtual ICollection<BrandPostDTO> Brands { get; set; } = new List<BrandPostDTO>();
		public virtual ICollection<SubCategoryPostDTO> SubCategories { get; set; } = new List<SubCategoryPostDTO>();
	}
}