using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class PriceHistoryPostDTO
	{
		public int ProdId { get; set; }
		public decimal Price { get; set; }
		public DateTime Date { get; set; }
	}
}
