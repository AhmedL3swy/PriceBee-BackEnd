﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UpdateUserDTO
    {
        [Required]
        [StringLength(40)]
        public string UserName { get; set; }


     

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string? Gender { get; set; }

     
        [MaxLength(30)]
        public string? Country { get; set; }

      
        public string? PhoneCode { get; set; }

        [MaxLength(10)]
        [RegularExpression(@"^1[1205][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        public string? Image { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}