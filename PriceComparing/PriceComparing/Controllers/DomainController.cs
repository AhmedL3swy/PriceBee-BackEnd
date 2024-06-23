using DataAccess.Models;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceComparing.UnitOfWork;

namespace PriceComparing.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DomainController : ControllerBase
	{
		private readonly UnitOfWOrks _unitOfWork;

		public DomainController(UnitOfWOrks unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDomains()
		{
			var domains = await _unitOfWork.DomainRepository.SelectAll();
			if (domains == null) return NotFound();

			List<DomainDTO> domainDTOs = new List<DomainDTO>();
			foreach (var domain in domains)
			{
				domainDTOs.Add(new DomainDTO()
				{
					Id = domain.Id,
					Name_Local = domain.Name_Local,
					Name_Global = domain.Name_Global,
					Description_Local = domain.Description_Local,
					Description_Global = domain.Description_Global,
					Url = domain.Url,
					Logo = domain.Logo
				});
			}
			return Ok(domainDTOs);
		}

        // GET: api/Domain/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllDomainsIgnoringFilters()
        {
            var domains = await _unitOfWork.DomainRepository.SelectAllIgnoringFiltersAsync();
            if (domains == null) return NotFound();

            List<DomainDTO> domainDTOs = new List<DomainDTO>();
            foreach (var domain in domains)
            {
                domainDTOs.Add(new DomainDTO()
                {
                    Id = domain.Id,
                    Name_Local = domain.Name_Local,
                    Name_Global = domain.Name_Global,
                    Description_Local = domain.Description_Local,
                    Description_Global = domain.Description_Global,
                    Url = domain.Url,
                    Logo = domain.Logo
                });
            }
            return Ok(domainDTOs);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetDomainById(int id)
		{
			var domain = await _unitOfWork.DomainRepository.SelectById(id);
			if (domain == null) return NotFound();

			DomainDTO domainDTO = new DomainDTO()
			{
				Id = domain.Id,
				Name_Local = domain.Name_Local,
				Name_Global = domain.Name_Global,
				Description_Local = domain.Description_Local,
				Description_Global = domain.Description_Global,
				Url = domain.Url,
				Logo = domain.Logo
			};
			return Ok(domainDTO);
		}

		[HttpPost]
		public async Task<IActionResult> AddDomain(DomainPostDTO domainDTO)
		{
			if (domainDTO == null) return BadRequest();

			Domain domain = new Domain()
			{
				Name_Local = domainDTO.Name_Local,
				Name_Global = domainDTO.Name_Global,
				Description_Local = domainDTO.Description_Local,
				Description_Global = domainDTO.Description_Global,
				Url = domainDTO.Url,
				Logo = domainDTO.Logo
			};
			await _unitOfWork.DomainRepository.Add(domain);
			_unitOfWork.savechanges();
			return Ok(domain);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDomain(int id, [FromBody] DomainPostDTO domainDTO)
		{
			if (domainDTO == null) return BadRequest();
			var domain = await _unitOfWork.DomainRepository.SelectById(id);
			if (domain == null) return NotFound();

			domain.Name_Local = domainDTO.Name_Local;
			domain.Name_Global = domainDTO.Name_Global;
			domain.Description_Local = domainDTO.Description_Local;
			domain.Description_Global = domainDTO.Description_Global;
			domain.Url = domainDTO.Url;
			domain.Logo = domainDTO.Logo;

			await _unitOfWork.DomainRepository.UpdateAsync(domain);
			return Ok(domain);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDomain(int id)
		{
			var domain = await _unitOfWork.DomainRepository.SelectById(id);
			if (domain == null) return NotFound();

			await _unitOfWork.DomainRepository.Delete(id);

			return Ok();
		}

        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteDomain(int id)
        {
            var domain = await _unitOfWork.DomainRepository.SelectById(id);
            if (domain == null) return NotFound();

            await _unitOfWork.DomainRepository.SoftDelete(id);
            return Ok();
        }

		[HttpGet("count")]
		public async Task<IActionResult> GetDomainCount()
		{
			var count = await _unitOfWork.DomainRepository.SelectAll();
			if (count == null) return NotFound();
			return Ok(count.Count());
		}

        [HttpGet("productscount")]
        public async Task<IActionResult> GetDomainsProductsCount()
        {
            var domains = await _unitOfWork.DomainRepository.SelectAll();
            if (domains == null) return NotFound();

            List<DomainProductsCountDTO> domainProductsCountList = domains.Select(domain => new DomainProductsCountDTO
            {
                DomainName = domain.Name_Global, // Assuming each domain has a Name property
                ProductCount = domain.ProductLinks.Count() // Assuming each domain has a Products collection
            }).ToList();

            return Ok(domainProductsCountList);
        }







    }
}
