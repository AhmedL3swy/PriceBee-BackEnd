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

        public DateOnly JoinDate { get; private set; } 

        [StringLength(255)]
        public string? PhoneCode { get; set; }

        [StringLength(255)]
        [RegularExpression(@"^01[1205][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string ?Image { get; set; }


        public virtual User ?User { set; get; }

        public AuthUser()
        {
            JoinDate = DateOnly.FromDateTime(DateTime.Now); 
        }
    }
}
