#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBtoclasses.Models;

[Table("PriceHistory")]
public partial class PriceHistory
{
    [Key]
    public int Id { get; set; }

    public int ProdId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("PriceHistories")]
    public virtual Product Prod { get; set; }
}