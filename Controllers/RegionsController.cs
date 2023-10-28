using learn.Data;
using learn.Models.Domain;
using learn.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DefaultDbContext dbContext;

        public RegionsController(DefaultDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET ALL REGIONS
        // GET: https://localhost:7027/api/Regions
        [HttpGet]
        public IActionResult GetAll()
        {
            // Get Data From Database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

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
        public IActionResult GetById([FromRoute] Guid id)
        {
            // Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.Find(id);

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
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map/Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to Create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update(
            [FromRoute] Guid id,
            [FromBody] UpdateRegionRequestDto updateRegionRequestDto
        )
        {
            // Check if region exists
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

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
    }
}
