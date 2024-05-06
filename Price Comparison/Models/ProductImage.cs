#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Price_Comparison.Models;

public partial class ProductImage
{
    [Key]
    public int Id { get; set; }

    public int ProdId { get; set; }

    [Required]
    public string Image { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("ProductImages")]
    public virtual ProductDetail Prod { get; set; }
}