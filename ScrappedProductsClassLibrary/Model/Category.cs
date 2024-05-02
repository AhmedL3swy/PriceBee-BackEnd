#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBtoclasses.Models;

[Table("Category")]
public partial class Category
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

    [InverseProperty("Category")]
    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}