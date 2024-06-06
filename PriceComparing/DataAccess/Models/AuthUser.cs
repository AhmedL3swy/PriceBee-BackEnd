using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class AuthUser : IdentityUser
    {
       
        [Required]
        [StringLength(255)]
        public string FName { get; set; }

        [Required]
        [StringLength(255)]
        public string LName { get; set; }


        [Required]
        [StringLength(10)]
        public string ?Gender { get; set; }

        [Required]
        [StringLength(255)]
        public string ?Country { get; set; }

        public DateOnly JoinDate { get; set; }

        [StringLength(255)]
        public string? PhoneCode { get; set; }

        [StringLength(255)]
        public string? PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string ?Image { get; set; }

        [JsonIgnore]
        public virtual User ?User { set; get; }  
    }
}
