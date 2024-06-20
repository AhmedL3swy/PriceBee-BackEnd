namespace DTO
{
		public class CombinedProductDetailDTO
		{
			public int ProductId { get; set; }
			public string ProductName_Local { get; set; }
			public string ProductName_Global { get; set; }
			public string ProductDescription_Local { get; set; }
			public string ProductDescription_Global { get; set; }
			public string SubCategoryName { get; set; }
			public string BrandName { get; set; }
			public DateTime LastUpdated { get; set; }
			public DateTime LastScraped { get; set; }
			public List<string> Images { get; set; }
			public List<ProductLinkDTO2> Links { get; set; }

			public CombinedProductDetailDTO()
			{
				Images = new List<string>();
				Links = new List<ProductLinkDTO2>();
			}
		}

	public class ProductLinkDTO2
	{
		public string DomainName { get; set; }
		public string DomainLogo { get; set; }
		public string ProductLink { get; set; }
		public decimal Price { get; set; }
		public decimal? Rating { get; set; }
	}
}
