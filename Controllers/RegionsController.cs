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
    }
}
