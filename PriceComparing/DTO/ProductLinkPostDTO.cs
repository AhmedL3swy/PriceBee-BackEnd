using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class ProductLinkPostDTO
	{
		public int ProdId { get; set; }
		public int DomainId { get; set; }
		public string ProductLink1 { get; set; }
		public string Status { get; set; }
		public DateTime LastUpdated { get; set; }
		public DateTime LastScraped { get; set; }
	}
}
