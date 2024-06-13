using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.Repository;
using PriceComparing.UnitOfWork;
using System.Collections;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWOrks unitOfWork;
        private readonly UserRepoNonGenric OnlyUser;

        public UserController(UnitOfWOrks _unitOfWork, UserRepoNonGenric _onlyUser)
        {
            unitOfWork = _unitOfWork;
            OnlyUser = _onlyUser;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<AuthUser> AuthUsers = await unitOfWork.AuthUserRepository.SelectAll();
            if (AuthUsers == null) return NotFound();

            var users = AuthUsers.Select(a => new
            {
                FirstName = a.FName,
                LastName = a.LName,
                Gender = a.Gender,
                Country = a.Country,
                DateOfBirth = a.DateOfBirth,
                Email = a.Email,
                UserName = a.UserName,
                PhoneCode = a.PhoneCode,
                PhoneNumber = a.PhoneNumber,
            });

            return Ok(users);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserOnly()
        {
            List<AuthUser> AuthUsers = OnlyUser.GetOnlyUser();
            if (AuthUsers == null) return NotFound();

            var users = AuthUsers.Select(a => new
            {
                id = a.Id,
                FirstName = a.FName,
                LastName = a.LName,
                Gender = a.Gender,
                Country = a.Country,
                DateOfBirth = a.DateOfBirth,
                Email = a.Email,
                UserName = a.UserName,
                PhoneCode = a.PhoneCode,
                PhoneNumber = a.PhoneNumber,
            });

            return Ok(users);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminOnly()
        {
            List<AuthUser> AuthUsers = OnlyUser.GetOnlyAdmin();
            if (AuthUsers == null) return NotFound();

            var users = AuthUsers.Select(a => new
            {
                id = a.Id,
                FirstName = a.FName,
                LastName = a.LName,
                Gender = a.Gender,
                Country = a.Country,
                DateOfBirth = a.DateOfBirth,
                Email = a.Email,
                UserName = a.UserName,
                PhoneCode = a.PhoneCode,
                PhoneNumber = a.PhoneNumber,
            });

            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            AuthUser AuthUser = await OnlyUser.GetById(id);
            if (AuthUser == null) return NotFound();

            var user = new 
            {
                FirstName = AuthUser.FName,
                LastName = AuthUser.LName,
                Gender = AuthUser.Gender,
                Country = AuthUser.Country,
                DateOfBirth = AuthUser.DateOfBirth,
                Email = AuthUser.Email,
                UserName = AuthUser.UserName,
                PhoneCode = AuthUser.PhoneCode,
                PhoneNumber = AuthUser.PhoneNumber,
            };

            return Ok(user);
        }
    }

}