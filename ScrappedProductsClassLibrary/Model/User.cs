#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DBtoclasses.Models;

[Index("Email", Name = "UQ__Users__A9D10534A011E897", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string FName { get; set; }

    [Required]
    [StringLength(255)]
    public string LName { get; set; }

    [Required]
    [StringLength(255)]
    public string Email { get; set; }

    [Required]
    [StringLength(255)]
    public string Password { get; set; }

    [Required]
    [StringLength(10)]
    public string Gender { get; set; }

    [Required]
    [StringLength(255)]
    public string Country { get; set; }

    [Column(TypeName = "date")]
    public DateTime JoinDate { get; set; }

    [StringLength(255)]
    public string PhoneCode { get; set; }

    [StringLength(255)]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfBirth { get; set; }

    public string Image { get; set; }

    [StringLength(255)]
    public string Role { get; set; }

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