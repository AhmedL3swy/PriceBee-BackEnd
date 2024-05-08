#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

[Table("SubCategory")]
public partial class SubCategory
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name_Local { get; set; }

    [Required]
    [StringLength(255)]
    [Unicode(false)]
    public string Name_Global { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("SubCategories")]
    public virtual Category Category { get; set; }

    [InverseProperty("SubCategory")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}