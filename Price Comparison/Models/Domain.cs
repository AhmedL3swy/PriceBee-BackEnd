﻿#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Price_Comparison.Models;

[Table("Domain")]
public partial class Domain
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

    [Required]
    public string Url { get; set; }

    [Required]
    public string Logo { get; set; }

    [InverseProperty("Domain")]
    public virtual ICollection<ProductLink> ProductLinks { get; set; } = new List<ProductLink>();
}