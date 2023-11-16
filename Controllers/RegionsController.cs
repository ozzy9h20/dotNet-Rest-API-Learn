using learn.Models.Domain;
using learn.Models.DTO;
using learn.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7027/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(
                    new RegionDto
                    {
                        Id = regionDomain.Id,
                        Code = regionDomain.Code,
                        Name = regionDomain.Name,
                        RegionImageUrl = regionDomain.RegionImageUrl
                    }
                );
            }

            // Return DTOs
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:7027/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Region Domain Model From Database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            // Return DTO Back to client
            return Ok(regionDto);
        }

        // POST TO CREATE NEW REGION
        // POST: https://localhost:7027/api/Regions
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to Create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model Back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // UPDATE REGION
        // PUT: https://localhost:7027/api/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto
        )
        {
            // Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Name = updateRegionRequestDto.Name,
                Code = updateRegionRequestDto.Code,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl
            };

            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        // DELETE REGION
        // Delete: https://localhost:7027/api/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return Deleted Region Back
            // Map Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}
