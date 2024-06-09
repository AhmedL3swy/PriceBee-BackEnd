using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PriceHistoryController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public PriceHistoryController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPriceHistories()
		{
			var priceHistories = await _unitOfWork.PriceHistoryRepository.SelectAll();
			if (priceHistories == null) return NotFound();

			List<PriceHistoryDTO> priceHistoryDTOs = new List<PriceHistoryDTO>();
			foreach (var priceHistory in priceHistories)
			{
				priceHistoryDTOs.Add(new PriceHistoryDTO()
				{
					Id = priceHistory.Id,
					ProdId = priceHistory.ProdId,
					Price = priceHistory.Price,
					Date = priceHistory.Date
				});
			}
			return Ok(priceHistoryDTOs);
		}

        // GET: api/PriceHistory/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllPriceHistoriesIgnoringFilters()
        {
            var priceHistories = await _unitOfWork.PriceHistoryRepository.SelectAllIgnoringFiltersAsync();
            if (priceHistories == null) return NotFound();

            List<PriceHistoryDTO> priceHistoryDTOs = new List<PriceHistoryDTO>();
            foreach (var priceHistory in priceHistories)
            {
                priceHistoryDTOs.Add(new PriceHistoryDTO()
                {
                    Id = priceHistory.Id,
                    ProdId = priceHistory.ProdId,
                    Price = priceHistory.Price,
                    Date = priceHistory.Date
                });
            }
            return Ok(priceHistoryDTOs);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetPriceHistoryById(int id)
		{
			var priceHistory = await _unitOfWork.PriceHistoryRepository.SelectById(id);
			if (priceHistory == null) return NotFound();

			PriceHistoryDTO priceHistoryDTO = new PriceHistoryDTO()
			{
				Id = priceHistory.Id,
				ProdId = priceHistory.ProdId,
				Price = priceHistory.Price,
				Date = priceHistory.Date
			};
			return Ok(priceHistoryDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddPriceHistory(PriceHistoryPostDTO priceHistoryDTO)
		{
			if (priceHistoryDTO == null) return BadRequest();

			PriceHistory priceHistory = new PriceHistory()
			{
				ProdId = priceHistoryDTO.ProdId,
				Price = priceHistoryDTO.Price,
				Date = priceHistoryDTO.Date
			};
			await _unitOfWork.PriceHistoryRepository.Add(priceHistory);
			_unitOfWork.savechanges();
			return Ok(priceHistory);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdatePriceHistory(int id, [FromBody] PriceHistoryPostDTO priceHistoryDTO)
		{
			if (priceHistoryDTO == null) return BadRequest();
			var priceHistory = await _unitOfWork.PriceHistoryRepository.SelectById(id);
			if (priceHistory == null) return NotFound();

			priceHistory.ProdId = priceHistoryDTO.ProdId;
			priceHistory.Price = priceHistoryDTO.Price;
			priceHistory.Date = priceHistoryDTO.Date;

			await _unitOfWork.PriceHistoryRepository.UpdateAsync(priceHistory);
			PriceHistoryDTO priceHistoryUpdated = new PriceHistoryDTO()
			{
				Id = priceHistory.Id,
				ProdId = priceHistory.ProdId,
				Price = priceHistory.Price,
				Date = priceHistory.Date
			};
			return Ok(priceHistoryUpdated);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePriceHistory(int id)
		{
			var priceHistory = await _unitOfWork.PriceHistoryRepository.SelectById(id);
			if (priceHistory == null) return NotFound();

			await _unitOfWork.PriceHistoryRepository.Delete(id);
			return Ok();
		}
	}
}
