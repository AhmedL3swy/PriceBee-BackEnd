using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.AuthModels;
using PriceComparing.Services;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthServices authserv;
        public AccountController(IAuthServices _authserv)
        {
            authserv = _authserv;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegsiterUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var AuthResult = await authserv.Register(user);
            if (!AuthResult.IsAuthenticated)
                return BadRequest(AuthResult.Message);

            return Ok(AuthResult);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var AuthResult = await authserv.Login(user);
            if (!AuthResult.IsAuthenticated)
                return BadRequest(AuthResult.Message);

            return Ok(AuthResult);
        }

        [HttpPost("Addrole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] RoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authserv.AssignRole(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
    }
}
