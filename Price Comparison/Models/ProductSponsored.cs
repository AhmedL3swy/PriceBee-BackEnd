#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBtoclasses.Models;

[Table("ProductSponsored")]
public partial class ProductSponsored
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Cost { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime StartDate { get; set; }

    public int Duration { get; set; }

    public int ProdId { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("ProductSponsoreds")]
    public virtual ProductDetail Prod { get; set; }
}