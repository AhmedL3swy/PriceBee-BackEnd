using BCrypt.Net;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SeedData
{
    public static class UserData
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            var password = "12346789@Ahmed";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            modelBuilder.Entity<AuthUser>().HasData(new AuthUser
            {
                UserName = "AhmedMostafa",
                NormalizedUserName = "Ahmed",
                Email = "AhmedMostafa@gmail.com",
                NormalizedEmail = "AHMEDMOSTAFA@GMAIL.COM",
                EmailConfirmed = true,
                FName = "Ahmed",
                LName = "Mostafa",
                Gender = "Male",
                Country = "Egypt",
                DateOfBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                PhoneCode = "+20",
                PhoneNumber = "1149147981",
                Image = "path_to_image",
                PasswordHash = hashedPassword
            });
        }
    }
}
