using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PriceComparing.AuthModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PriceComparing.Services
{
    public class AuthService : IAuthServices
    {
        private readonly UserManager<AuthUser> userManager;
        private readonly JWT _jwt;
        private readonly RoleManager<IdentityRole> roleManager;

        public AuthService(UserManager<AuthUser> _userManager, IOptions<JWT> jwt, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            _jwt = jwt.Value;
            roleManager = _roleManager;
        }

        public async Task<AuthModel> Register(RegsiterUserDTO user)
        {
            if (await userManager.FindByEmailAsync(user.Email) != null)
                return new AuthModel { Message = "this Eamil already exist" };
            if (await userManager.FindByNameAsync(user.UserName) != null)
                return new AuthModel { Message = "this UserName already exist" };
            var CreatedUser = new AuthUser
            {
                FName = user.FirstName,
                LName = user.LastName,
                Email = user.Email,
                PasswordHash = user.Password,
                UserName = user.UserName,
                Country =user.Country,
                DateOfBirth =user.DateOfBirth,
                PhoneCode = user.PhoneNumber,
                PhoneNumber =user.PhoneNumber,
               // JoinDate = DateOnly.FromDateTime(DateTime.Now),
                Gender =user.Gender,
            };

            var identityResult = await userManager.CreateAsync(CreatedUser, user.Password);
            string errors = string.Empty;
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    errors += $"{error.Description},";
                return new AuthModel() { Message = errors };
            }

            await userManager.AddToRoleAsync(CreatedUser, "User");

            var jwtSecurityToken = await CreateJwtToken(CreatedUser);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Username = user.UserName ,
                Message = "Success"

            };

        }


        private async Task<JwtSecurityToken> CreateJwtToken(AuthUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }



        public async Task<AuthModel> Login(LoginUserDTO LoginedUser)
        {
            AuthModel authmodel = new AuthModel();
            var user = await userManager.FindByEmailAsync(LoginedUser.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, LoginedUser.Password))
            {
                authmodel.Message = "entered wrong Password Or UserName";
                return authmodel;
            }

            var Token = await CreateJwtToken(user);
            var Roles = await userManager.GetRolesAsync(user);

         
            authmodel.IsAuthenticated = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(Token);
            authmodel.Email = user.Email;
            authmodel.Username = user.UserName;
            authmodel.ExpiresOn = Token.ValidTo;
            authmodel.Roles = Roles.ToList();
            authmodel.Message = "Success";

            return authmodel;
        }

        public async Task<string> AssignRole(RoleModel model)
        {
            var user = await userManager.FindByIdAsync(model.UserID);

            if (user is null || !await roleManager.RoleExistsAsync(model.RoleName))
                return "Invalid user ID or Role";
            if (await userManager.IsInRoleAsync(user, model.RoleName))
                return "Already Assigned to this Role";
            var result = await userManager.AddToRoleAsync(user, model.RoleName);
            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }

    }
}
