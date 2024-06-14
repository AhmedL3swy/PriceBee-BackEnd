using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavProdController : ControllerBase
    {
        private readonly UnitOfWOrks _unitOfWork;

        public UserFavProdController(UnitOfWOrks unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/UserFavProd
        [HttpGet]
        public async Task<IActionResult> GetUserFavProd()
        {
            var userFavProds = await _unitOfWork.UserFavProdRepo.SelectAll();
            if (userFavProds == null) return NotFound();

            return Ok(userFavProds);
        }

        // GET: api/UserFavProd/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFavProdById(int id)
        {
            var userFavProd = await _unitOfWork.UserFavProdRepo.SelectById(id);
            if (userFavProd == null) return NotFound();

            return Ok(userFavProd);
        }

        // POST: api/UserFavProd
        //[HttpPost]
        //public async Task<IActionResult> AddUserFavProd(UserFavProd userFavProd)
        //{
        //    if (userFavProd == null) return BadRequest();

        //    await _unitOfWork.UserFavProdRepo.Insert(userFavProd);
        //    await _unitOfWork.Save();

        //    return Ok(userFavProd);
        //}
        //[HttpPost]
    }
}
