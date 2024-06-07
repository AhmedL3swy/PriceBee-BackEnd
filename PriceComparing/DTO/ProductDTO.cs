using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class ProductDTO
	{
		public int Id { get; set; }
		public string? Name_Local { get; set; }
		public string? Name_Global { get; set; }
		public string? Description_Local { get; set; }
		public string? Description_Global { get; set; }
		public int SubCategoryId { get; set; }
		public int? BrandId { get; set; }
	}
}
