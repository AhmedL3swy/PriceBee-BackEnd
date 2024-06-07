﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index("ProdId", Name = "IX_ProductImages_ProdId")]
public partial class ProductImage : ISoftDeletable
{
    [Key]
    public int Id { get; set; }

    public int ProdId { get; set; }

    [Required]
    public string Image { get; set; }

    [ForeignKey("ProdId")]
    [InverseProperty("ProductImages")]
    public virtual Product Prod { get; set; }
}