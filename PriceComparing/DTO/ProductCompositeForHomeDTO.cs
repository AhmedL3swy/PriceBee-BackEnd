namespace DTO
{
		public class ProductCompositeForHomeDTO
    {
			public int ProductId { get; set; }
			public string ProductName_Local { get; set; }
			public string ProductName_Global { get; set; }
		
		
		
			public string Images { get; set; }
			public List<ProductLinkDTOForHOme> Links { get; set; }
		public decimal MinPrice { get; set; }
		public string MinPriceDomainLogo { get; set; }
		public string MinPriceBrandName { get; set; }
		public int DomainCount { get; set; }


            public ProductCompositeForHomeDTO()
			{
				
				Links = new List<ProductLinkDTOForHOme>();

            }
		}

	public class ProductLinkDTOForHOme
	{
		public string DomainName { get; set; }
		public string DomainLogo { get; set; }
		public string ProductLink { get; set; }
		public decimal Price { get; set; }
		
	}
}
