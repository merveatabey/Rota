using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourActivityController : ControllerBase
    {
        private readonly ITourActivityService _tourActivityService;

        public TourActivityController(ITourActivityService tourActivityService)
        {
            _tourActivityService = tourActivityService;
        }

        // GET api/TourActivity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourActivityDto>>> GetAll()
        {
            var activities = await _tourActivityService.GetAllAsync();
            return Ok(activities);
        }

        // GET api/TourActivity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TourActivityDto>> GetById(int id)
        {
            var activity = await _tourActivityService.GetByIdAsync(id);
            if (activity == null)
                return NotFound($"TourActivity with id {id} not found.");

            return Ok(activity);
        }

        // POST api/TourActivity
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TourActivityDto dto)
        {
            await _tourActivityService.CreateAsync(dto);
            return Ok("Tour activity created successfully");
        }

        // PUT api/TourActivity/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] TourActivityDto dto)
        {
            dto.Id = id;

            var existing = await _tourActivityService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"TourActivity with id {id} not found.");

            await _tourActivityService.UpdateAsync(dto);
            return Ok("Tour activity updated successfully");
        }

        // DELETE api/TourActivity/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existing = await _tourActivityService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"TourActivity with id {id} not found.");

            await _tourActivityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
