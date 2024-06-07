using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class ProductSponsoredDTO
	{
		public int Id { get; set; }
		public decimal Cost { get; set; }
		public DateTime StartDate { get; set; }
		public int Duration { get; set; }
		public int ProdId { get; set; }
	}
}
