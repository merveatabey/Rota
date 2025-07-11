using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Controllers
{
    [Route("api/[controller]")]
    public class TourController : ControllerBase
    {
        private readonly ITourService _tourService;
        public TourController(ITourService tourService)
        {
            _tourService = tourService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tours = await _tourService.GetAllAsync();
            return Ok(tours);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tour = await _tourService.GetByIdAsync(id);
            if(tour == null)
            {
                return NotFound();
            }
            return Ok(tour);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TourDto dto)
        {
            await _tourService.CreateAsync(dto);
            return Ok("Tour created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TourDto dto)
        {
            dto.Id = id;
            await _tourService.UpdateAsync(dto);
            return Ok("Tour updated succesfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _tourService.DeleteAsync(id);
            return Ok("Tour deleted successfully");
        }


        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetDetails(int id)
        {
            try
            {
                var tourDetails = await _tourService.GetTourDetailsAsync(id);
                return Ok(tourDetails);

            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
            

    }
}

