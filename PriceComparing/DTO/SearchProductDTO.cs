using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	//public class SearchProductDTO
	//{
	//    public int Product_Id { get; set; }
	//    public string? Product_Name_Local { get; set; }
	//    public string? Product_Name_Global { get; set; }
	//    public string? Product_Description_Local { get; set; }
	//    public string? Product_Description_Global { get; set; }
	//    public int Product_NumberOfClicks { get; set; }
	//    public BrandPostDTO brandPostDTO { get; set; }
	//    //  public int Brand_Id { get; set; }
	//    public SubCategoryPostDTO subCategoryPostDTO { get; set; }
	//    // list of price history dto 
	//    // public List<PriceHistoryDTO> priceHistoryDTOs { get; set; } = new List<PriceHistoryDTO>();
	//    // list of product image dto
	//    public List<ProductImageDTO> productImageDTOs { get; set; } = new List<ProductImageDTO>();
	//    // list of product link dto
	//    public List<ProudctLinkWithDetailsDTO> productLinkDTOs { get; set; } = new List<ProudctLinkWithDetailsDTO>();

	//}
	//public class ProudctLinkWithDetailsDTO
	//{
	//    // Product Link
	//    public int Link_Id { get; set; }
	//    // public int ProdId { get; set; }
	//    public int Link_DomainId { get; set; }
	//    public string ProductLink { get; set; }
	//    //public string Status { get; set; }
	//    //public DateTime LastUpdated { get; set; }
	//    //public DateTime LastScraped { get; set; }

	//    // Product Details (ProductDet
	//    // public int Id { get; set; }
	//    public string ProductDet_Name_Local { get; set; }
	//    public string ProductDet_Name_Global { get; set; }
	//    public string ProductDet_Description_Local { get; set; }
	//    public string ProductDet_Description_Global { get; set; }
	//    public decimal ProductDet_Price { get; set; }
	//    public decimal? ProductDet_Rating { get; set; }
	//    public bool ProductDet_isAvailable { get; set; }
	//    // public string Brand { get; set;         
	//}

	public class SearchProductDTO
	{
		public int Product_Id { get; set; }
		public string? Product_Name_Local { get; set; }
		public string? Product_Name_Global { get; set; }
		public string? Product_Description_Local { get; set; }
		public string? Product_Description_Global { get; set; }
		public DateTime AddedDate { get; set; }
        public int Product_NumberOfClicks { get; set; }
        public BrandPostDTO brandPostDTO { get; set; }
		public SubCategoryPostDTO subCategoryPostDTO { get; set; }
		public List<ProductImageDTO> productImageDTOs { get; set; } = new List<ProductImageDTO>();
		public List<ProudctLinkWithDetailsDTO> productLinkDTOs { get; set; } = new List<ProudctLinkWithDetailsDTO>();

        // most favorite product
		public int numberOfFavorites { get; set; }
        // average rating
        public decimal? averageRating { get; set; }

		// Most minimum price 
		public decimal? mostMinimumPrice { get; set; }
    }

    public class ProudctLinkWithDetailsDTO
	{
		public int Link_Id { get; set; }
		//public int Link_DomainId { get; set; }
		//public string Domain_Logo { get; set; }
        public string ProductLink { get; set; }
		public string ProductDet_Name_Local { get; set; }
		public string ProductDet_Name_Global { get; set; }
		public string ProductDet_Description_Local { get; set; }
		public string ProductDet_Description_Global { get; set; }
		public decimal ProductDet_Price { get; set; }
		public decimal? ProductDet_Rating { get; set; }
		public bool ProductDet_isAvailable { get; set; }
		public DateTime LastUpdated { get; set; }

        // Domain Required Data
        // 1. ID
		public int Link_DomainId { get; set; }
        // 2. Domain Name
        public string? Domain_Logo { get; set; }

        // then get the sponsered products
        public List<ProductSponsoredDTO> productSponsoredDTOs { get; set; } = new List<ProductSponsoredDTO>();


    }

    /*
    public int Id { get; set; }

    public decimal Cost { get; set; }

    public DateTime StartDate { get; set; }

    public int Duration { get; set; }

    public int ProdDetId { get; set; }

    public virtual ProductDetail ProdDet { get; set; }
	*/

}
