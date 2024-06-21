using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PriceComparing.Repository;
using PriceComparing.Services;
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
        private readonly IAuthServices authServices;
        private readonly IUserServices userServices;


        public UserController(UnitOfWOrks _unitOfWork, UserRepoNonGenric _onlyUser,IAuthServices _authserv, IUserServices _userServ)
        {
            unitOfWork = _unitOfWork;
            OnlyUser = _onlyUser;
            authServices = _authserv;
            userServices = _userServ;
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
            List<AuthUser> AuthUsers = unitOfWork.UserRepoNonGenric.GetOnlyUser();
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
            List<AuthUser> AuthUsers = unitOfWork.UserRepoNonGenric.GetOnlyAdmin();
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
            AuthUser AuthUser = await unitOfWork.UserRepoNonGenric.GetById(id);
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
        [HttpPost("AssignAdmin")]
        public async Task<IActionResult> AssignAdminRole(string ID)
        {
            string message = await authServices.AssignAdminRole(ID);
            return Ok(message);
        }

        [HttpPost("RemoveAdmin")]
        public async Task<IActionResult> RemoveAdminRole(string ID)
        {
            string message = await authServices.AssignUserRoleAgain(ID);
            return Ok(message);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO user, string id)
        {
            var result = await userServices.UpdateUserAsync(user, id);
            if(!ModelState.IsValid) return BadRequest(ModelState);

            if (result.IsUpdated == false)
                return BadRequest(result.message); 
            else return Ok(result.message);
        }

        [HttpGet("FavProduct")]
        public async Task<IActionResult> GetFavProds ( string id)
        {
            var x= await userServices.getUserFavProd(id);
            return Ok(x);
        }

        [HttpPost("FavProduct")]
        public async Task<IActionResult> GetFavProds(int id, string Userid)
        {
            await userServices.AddUserFavProd(id, Userid);
            return Ok();
        }


        [HttpGet("HistoryProduct")]
        public async Task<IActionResult> getHisroty(string id)
        {
            var x = await userServices.getUserHistoryProd(id);
            return Ok(x);
        }

        [HttpPost("HistoryProduct")]
        public async Task<IActionResult> AddHistoryProds(int id, string Userid)
        {
            await userServices.AddUserHistoryProd(id, Userid);
            return Ok();
        }


        [HttpGet("AlertProduct")]
        public async Task<IActionResult> getAlertProd(string id)
        {
            var x = await userServices.getUserAlert(id);
            return Ok(x);
        }


        [HttpPost("AlertProduct")]
        public async Task<IActionResult> AddAlertProd(int id, string Userid)
        {
            await userServices.AddUserAlertProd(id, Userid);
            return Ok();
        }


    }

}