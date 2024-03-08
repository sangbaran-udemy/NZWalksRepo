using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:portNumber/api/Regions
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:portNumber/api/Regions
        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            var regions = await regionRepository.GetAllAsync();
            var regionsDTO = mapper.Map<List<Models.DTO.RegionDTO>>(regions);
            
            return Ok(regionsDTO);            
        }

        // GET: https://localhost:portNumber/api/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var region = await regionRepository.GetAsync(id);

            if (region != null)
            {
                var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
                return Ok(regionDTO);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequestDTO addRegionRequest)
        {
            // Convert DTO to Domain model..
            var region = mapper.Map<Models.Domain.Region>(addRegionRequest);

            // Pass data to Repo..
            region = await regionRepository.AddAsync(region);

            // Convert Domain model back to DTO..
            var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Delete the region..
            var region = await regionRepository.DeleteAsync(id);

            // Convert the domain model back to DTO..
            if (region != null)
            {
                var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
                return Ok(regionDTO);
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequest)
        {
            // Convert the DTO to a Domain model..
            var region = mapper.Map<Models.Domain.Region>(updateRegionRequest);

            // Update the Domain model..
            region = await regionRepository.UpdateAsync(id, region);

            // Convert the Domain to a DTO..
            if (region != null)
            {
                var regionDTO = mapper.Map<Models.DTO.RegionDTO>(region);
                return Ok(regionDTO);
            }

            return NotFound();
        }
    }
}
