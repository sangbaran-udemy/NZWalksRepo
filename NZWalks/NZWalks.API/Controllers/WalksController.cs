using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
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
        // POST: https://localhost:portNumber/api/Walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            // Map Domain to DTO..
            var walkDomainModel = mapper.Map<Models.Domain.Walk>(addWalkRequestDTO);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            // Map Domain model to DTO..
            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }
        // GET: https://localhost:portNumber/api/Walks?filterOn=Name&filterQuery=Track&sortOn=Name&sortBy=ascending
        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending = false,
            [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 100)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? false, pageNo, pageSize);

            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomainModel);

            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetAsync(id);
            if (walkDomainModel == null)
                return NotFound();

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            var walkDomainModel = mapper.Map<Models.Domain.Walk>(updateWalkRequestDTO);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
                return NotFound();

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);

            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.DeleteAsync(id);

            if(walkDomainModel == null)
                return NotFound();

            var walkDTO = mapper.Map<WalkDTO>(walkDomainModel);
            return Ok(walkDTO);

        }
    }
}
