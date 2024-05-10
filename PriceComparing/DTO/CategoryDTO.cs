using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace DTO
{
	public class CategoryDTO
	{
		public int Id { get; set; }
		public required string Name_Local { get; set; }
		public required string Name_Global { get; set; }
		public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();
		public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
	}
}


//public partial class Category
//{
//	[Key]
//	public int Id { get; set; }

//	[Required]
//	[StringLength(255)]
//	public string Name_Local { get; set; }

//	[Required]
//	[StringLength(255)]
//	[Unicode(false)]
//	public string Name_Global { get; set; }

//	[InverseProperty("Category")]
//	public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

//	[InverseProperty("Category")]
//	public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
//}