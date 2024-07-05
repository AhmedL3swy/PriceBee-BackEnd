using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdFavUserController : ControllerBase
    {
        //private readonly UnitOfWOrks _unitOfWork;

        //public ProdFavUserController(UnitOfWOrks unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //}

        private readonly DbContext _db;
        public ProdFavUserController(DbContext db)
        {
            _db = db;
        }

        // GET: api/ProdFavUser
        //[HttpGet]
        //public async Task<IActionResult> GetProdFavUser()
        //{
        //    var prodFavUsers = await _db.Set<ProdFavUser>().ToListAsync();
        //    // await _db.Entry(ProdFavUser).Property("IsDeleted").CurrentValue
        //    if (prodFavUsers == null) return NotFound();

        //    return Ok(prodFavUsers);
        //}

    }
}
