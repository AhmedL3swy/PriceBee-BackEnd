﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("Product")]
[Index("BrandId", Name = "IX_Product_BrandId")]
[Index("SubCategoryId", Name = "IX_Product_SubCategoryId")]
public partial class Product : ISoftDeletable
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

    public string Description_Local { get; set; }

    [Unicode(false)]
    public string Description_Global { get; set; }

    public int SubCategoryId { get; set; }

    
    public int? BrandId { get; set; }

    [ForeignKey("BrandId")]
    [InverseProperty("Products")]
    public virtual Brand Brand { get; set; }

    [InverseProperty("Prod")]
    public virtual ICollection<PriceHistory> PriceHistories { get; set; } = new List<PriceHistory>();

    [InverseProperty("Prod")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Prod")]
    public virtual ICollection<ProductLink> ProductLinks { get; set; } = new List<ProductLink>();

    [ForeignKey("SubCategoryId")]
    [InverseProperty("Products")]
    public virtual SubCategory SubCategory { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("Prods")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();

    [ForeignKey("ProdId")]
    [InverseProperty("Prods1")]
    public virtual ICollection<User> Users1 { get; set; } = new List<User>();

    [ForeignKey("ProdId")]
    [InverseProperty("ProdsNavigation")]
    public virtual ICollection<User> UsersNavigation { get; set; } = new List<User>();
}