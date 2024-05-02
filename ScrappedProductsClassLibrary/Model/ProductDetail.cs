#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBtoclasses.Models;

public partial class ProductDetail
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name_Local { get; set; }

    [Required]
    [StringLength(255)]
    public string Name_Global { get; set; }

    [Required]
    public string Description_Local { get; set; }

    [Required]
    public string Description_Global { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal? Rating { get; set; }

    public bool isAvailable { get; set; }

    [StringLength(255)]
    public string Brand { get; set; }

    [ForeignKey("Id")]
    [InverseProperty("ProductDetail")]
    public virtual ProductLink IdNavigation { get; set; }

    [InverseProperty("Prod")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Prod")]
    public virtual ICollection<ProductSponsored> ProductSponsoreds { get; set; } = new List<ProductSponsored>();
}