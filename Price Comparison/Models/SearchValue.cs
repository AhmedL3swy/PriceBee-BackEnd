#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Price_Comparison.Models;

public partial class SearchValue
{
    [Key]
    public int Id { get; set; }

    public int UserID { get; set; }

    [Column("SearchValue")]
    [StringLength(255)]
    public string SearchValue1 { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("SearchValues")]
    public virtual User User { get; set; }
}