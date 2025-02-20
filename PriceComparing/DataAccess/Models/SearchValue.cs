﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

[Index("UserID", Name = "IX_SearchValues_UserID")]
public partial class SearchValue : ISoftDeletable
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