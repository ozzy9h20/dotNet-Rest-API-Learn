using Asp.Versioning;
using AutoMapper;
using learn.CustomActionFilters;
using learn.Models.Domain;
using learn.Models.DTO;
using learn.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace learn.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion(1.0)]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpPost, MapToApiVersion(1.0)]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
            var createdWalk = await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(createdWalk));
        }

        [HttpGet, MapToApiVersion(1.0)]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn,
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 1000
        )
        {
            var walksDomainModel = await walkRepository.GetAllAsync(
                filterOn,
                filterQuery,
                sortBy,
                isAscending ?? true,
                pageNumber,
                pageSize
            );

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] UpdateWalkRequestDto updateWalkRequestDto
        )
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpDelete, MapToApiVersion(1.0)]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
    }
}
