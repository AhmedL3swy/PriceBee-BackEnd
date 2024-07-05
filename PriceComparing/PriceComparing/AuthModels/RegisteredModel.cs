using DataAccess.Models;
using System.ComponentModel.DataAnnotations;

namespace PriceComparing.AuthModels
{
    public class RegisteredModel
    {
        [Required, MaxLength(100)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]

        public string Password { get; set; }
        [Required, MaxLength(100)]

        public string FirstName { get; set; }
        [Required, MaxLength(100)]

        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"
                            , ErrorMessage = "Invalid email format.")]

        public string Email { get; set; }

        public string? Gender { get; set; }
        public string? Country { get; set; }

        public DateOnly JoinDate { get; set; }

       
        public string? PhoneCode { get; set; }

       
        public string? PhoneNumber { get; set; }

        public string? Image { get; set; }
        public DateOnly DateOfBirth { get; set; }

       // public User? User { get; set; }


    }
}
