﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	public class ProductDetailDTO
	{
		public int Id { get; set; }
		public string Name_Local { get; set; }
		public string Name_Global { get; set; }
		public string Description_Local { get; set; }
		public string Description_Global { get; set; }
		public decimal Price { get; set; }
		public decimal? Rating { get; set; }
		public bool isAvailable { get; set; }
		public string Brand { get; set; }
	}
}
