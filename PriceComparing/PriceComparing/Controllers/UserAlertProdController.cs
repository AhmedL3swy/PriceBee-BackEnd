using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAlertProdController : ControllerBase
    {
        private readonly UnitOfWOrks _unitOfWOrks;

        public UserAlertProdController(UnitOfWOrks unitOfWOrks)
        {
            _unitOfWOrks = unitOfWOrks;
        }

        // GET: api/UserAlertProd
        [HttpGet]
        public async Task<IActionResult> GetUserAlertProd()
        {
            var userAlertProds = await _unitOfWOrks.UserAlertProdRepo.SelectAll();
            if (userAlertProds == null) return NotFound();

            return Ok(userAlertProds);
        }

        // GET: api/UserAlertProd/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAlertProdById(int id)
        {
            var userAlertProd = await _unitOfWOrks.UserAlertProdRepo.SelectById(id);
            if (userAlertProd == null) return NotFound();

            return Ok(userAlertProd);
        }

        // POST: api/UserAlertProd
        //[HttpPost]
        //public async Task<IActionResult> AddUserAlertProd(UserAlertProd )
        //{
        //    if (userAlertProd == null) return BadRequest();

        //    await _unitOfWOrks.UserAlertProdRepo.Insert(userAlertProd);
        //    await _unitOfWOrks.Save();

        //    return Ok(userAlertProd);
        //}
        //[HttpPost]

    }
}
