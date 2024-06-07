﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
<<<<<<< HEAD
using Microsoft.AspNetCore.Identity;
=======
using DataAccess.Interfaces;
>>>>>>> Preview
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

<<<<<<< HEAD
//[Index("Email", Name = "UQ__Users__A9D10534A011E897", IsUnique = true)]
public partial class User 
=======
[Index("Email", Name = "UQ__Users__A9D10534A011E897", IsUnique = true)]
public partial class User : ISoftDeletable
>>>>>>> Preview
{
    [Key]
    public int Id { get; set; }

    public virtual AuthUser AuthenticatedUser { get; set; }

    [ForeignKey("AuthenticatedUser")]
    public string AuthUserID { get; set; }


    //[Required]
    //[StringLength(255)]
    //public string FName { get; set; }

    //[Required]
    //[StringLength(255)]
    //public string LName { get; set; }

    //[Required]
    //[StringLength(255)]
    //public string Email { get; set; }

    //[Required]
    //[StringLength(255)]
    //public string Password { get; set; }

    //[Required]
    //[StringLength(10)]
    //public string Gender { get; set; }

    //[Required]
    //[StringLength(255)]
    //public string Country { get; set; }

    //public DateOnly JoinDate { get; set; }

    //[StringLength(255)]
    //public string PhoneCode { get; set; }

    //[StringLength(255)]
    //public string PhoneNumber { get; set; }

    //public DateOnly? DateOfBirth { get; set; }

    //public string Image { get; set; }

    //[StringLength(255)]
    //public string Role { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<SearchValue> SearchValues { get; set; } = new List<SearchValue>();

    [ForeignKey("UserID")]
    [InverseProperty("Users")]
    public virtual ICollection<Product> Prods { get; set; } = new List<Product>();

    [ForeignKey("UserID")]
    [InverseProperty("Users1")]
    public virtual ICollection<Product> Prods1 { get; set; } = new List<Product>();

    [ForeignKey("UserID")]
    [InverseProperty("UsersNavigation")]
    public virtual ICollection<Product> ProdsNavigation { get; set; } = new List<Product>();
}