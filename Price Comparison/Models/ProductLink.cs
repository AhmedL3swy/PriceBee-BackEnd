#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Price_Comparison.Models;

public partial class ProductLink
{
    [Key]
    public int Id { get; set; }

    public int ProdId { get; set; }

    public int DomainId { get; set; }

    [Required]
    [Column("ProductLink")]
    public string ProductLink1 { get; set; }

    [Required]
    [StringLength(255)]
    public string Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastUpdated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastScraped { get; set; }

    [ForeignKey("DomainId")]
    [InverseProperty("ProductLinks")]
    public virtual Domain Domain { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("ProductLinks")]
    public virtual Product Prod { get; set; }

    [InverseProperty("IdNavigation")]
    public virtual ProductDetail ProductDetail { get; set; }
}