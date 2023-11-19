using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using learn.CustomActionFilters;
using learn.Models.Domain;
using learn.Models.DTO;
using learn.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger
        )
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7027/api/Regions/
        [HttpGet, MapToApiVersion(1.0)]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn = null,
            [FromQuery] string? filterQuery = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? isAscending = true,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000
        )
        {
            // Get Data From Database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                isAscending ?? true,
                pageNumber,
                pageSize
            );

            logger.LogInformation(
                $"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}"
            );

            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }

        // GET SINGLE REGION (Get Region By ID)
        // GET: https://localhost:7027/api/Regions/{id}
        [HttpGet, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer,Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            // Get Region Domain Model From Database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Return DTO Back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST TO CREATE NEW REGION
        // POST: https://localhost:7027/api/Regions
        [HttpPost, MapToApiVersion(1.0)]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use Domain Model to Create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            // Map Domain Model Back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // UPDATE REGION
        // PUT: https://localhost:7027/api/Regions/{id}
        [HttpPut, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto
        )
        {
            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exists
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }

        // DELETE REGION
        // Delete: https://localhost:7027/api/Regions/{id}
        [HttpDelete, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Return Deleted Region Back
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
