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

        // 1- Get all active domains
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

        // 2- Get all domains ignoring filters
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

		// 3- Get all soft deleted domains
		// GET: api/Domain/SoftDeleted
		[HttpGet("softdeleted")]
        public async Task<IActionResult> GetAllSoftDeletedDomains()
		{
			var domains = await _unitOfWork.DomainRepository.SelectAllSoftDeletedAsync();
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

        // 4- Get domain by id
        // GET: api/Domain/1
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

        // 5- Get domain by id ignoring filters
        // GET: api/Domain/ignore/1
        [HttpGet("ignore/{id}")]
        public async Task<IActionResult> GetDomainByIdIgnoringFilters(int id)
        {
            var domain = await _unitOfWork.DomainRepository.SelectByIdIgnoringFiltersAsync(id);
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

        // 6- Add domain
        // POST: api/Domain
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

        // 7- Update domain
        // PUT: api/Domain/1
        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateDomain(int id, [FromBody] DomainPostDTO domainPostDTO)
		{
			if (domainPostDTO == null) return BadRequest();
			Domain domain = await _unitOfWork.DomainRepository.SelectById(id);
			if (domain == null) return NotFound();

			domain.Name_Local = domainPostDTO.Name_Local;
			domain.Name_Global = domainPostDTO.Name_Global;
			domain.Description_Local = domainPostDTO.Description_Local;
			domain.Description_Global = domainPostDTO.Description_Global;
			domain.Url = domainPostDTO.Url;
			domain.Logo = domainPostDTO.Logo;

			await _unitOfWork.DomainRepository.UpdateAsync(domain);

            BrandDTO brandDTO = new BrandDTO()
            {
                Id = domain.Id,
                Name_Local = domain.Name_Local,
                Name_Global = domain.Name_Global,
                Description_Local = domain.Description_Local,
                Description_Global = domain.Description_Global,
                Logo = domain.Logo,
                LogoUrl = domain.Logo
            };
            return Ok(domain);
		}

        // 8- Delete domain
        // DELETE: api/Domain/1
        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDomain(int id)
		{
			var domain = await _unitOfWork.DomainRepository.SelectById(id);
			if (domain == null) return NotFound();

			await _unitOfWork.DomainRepository.Delete(id);

			return Ok();
		}

        // 9- Soft delete domain
        // DELETE: api/Domain/SoftDelete/1
        [HttpDelete("SoftDelete/{id}")]
        public async Task<IActionResult> SoftDeleteDomain(int id)
        {
            var domain = await _unitOfWork.DomainRepository.SelectById(id);
            if (domain == null) return NotFound();

            await _unitOfWork.DomainRepository.SoftDelete(id);
            return Ok();
        }

        // 10- Restore domain
        // PUT: api/Domain/Restore/1
        [HttpPut("Restore/{id}")]
        public async Task<IActionResult> RestoreDomain(int id)
        {
            var domain = await _unitOfWork.DomainRepository.SelectByIdIgnoringFiltersAsync(id);
            if (domain == null) return NotFound("Domain not found or already active.");
            await _unitOfWork.DomainRepository.Restore(domain);
            return Ok();
        }

        #region Dashboard_Counters
        // 1- Get domain count
        [HttpGet("count")]
		public async Task<IActionResult> GetDomainCount()
		{
			var count = await _unitOfWork.DomainRepository.SelectAll();
			if (count == null) return NotFound();
			return Ok(count.Count());
		}

        // 2- Get active domain count
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
        #endregion






    }
}
