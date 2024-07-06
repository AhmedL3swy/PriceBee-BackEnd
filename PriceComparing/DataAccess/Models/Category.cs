﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Table("Category")]
public partial class Category : ISoftDeletable
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
    public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

    [InverseProperty("Category")]
    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}