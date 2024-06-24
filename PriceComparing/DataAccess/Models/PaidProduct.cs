//using DataAccess.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess.Models
//{
//    [Table("PaidProduct")]
//    [Index("BrandId", Name = "IX_Product_BrandId")]
//    [Index("SubCategoryId", Name = "IX_Product_SubCategoryId")]
//    public partial class PaidProduct : ISoftDeletable
//    {
//        [Key]
//        public int Id { get; set; }

//        [Required]
//        [StringLength(255)]
//        public string? Name_Local { get; set; } // Made nullable

//        [Required]
//        [StringLength(255)]
//        [Unicode(false)]
//        public string? Name_Global { get; set; } // Made nullable

//        public string? Description_Local { get; set; } // Made nullable

//        [Unicode(false)]
//        public string? Description_Global { get; set; } // Made nullable

//        public int SubCategoryId { get; set; }

//        public int? BrandId { get; set; }

//        [ForeignKey("BrandId")]
        
//        public virtual Brand? Brand { get; set; } // Made nullable

    
//        public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();

        
//        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

       
//        public virtual ICollection<ProductLink> ProductLinks { get; set; } = new List<ProductLink>();

        
       
//        public virtual SubCategory? SubCategory { get; set; } // Made nullable

       
//        public virtual ICollection<User> UserAlertProd { get; set; } = new List<User>();

       
//        public virtual ICollection<User> UserHistoryProd { get; set; } = new List<User>();

       
//        public virtual ICollection<User> UserFavProd { get; set; } = new List<User>();

//        // test
//        public virtual IEnumerable<UserAlertProd>? UserAlertProds { get; set; } // Made nullable
//        public virtual IEnumerable<UserHistoryProd>? UserHistoryProds { get; set; } // Made nullable
//        public virtual IEnumerable<UserFavProd>? UserFavProds { get; set; } // Made nullable

//        // Added properties for paid product
//        public bool IsPaid { get; set; }
//        public int? Duration { get; set; }
//        public DateTime? StartTime { get; set; }
//        public DateTime? EndTime { get; set; }
//        public decimal? Budget { get; set; }
//    }
//}
