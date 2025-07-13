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
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }


        [HttpGet]
        public async Task<IEnumerable<ReservationDto>> GetAll()
        {
            return await _reservationService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var reservation = await _reservationService.GetByIdAsync(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with id {id} not found.");
            }

            return reservation;
        }


        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create([FromBody] ReservationDto dto)
        {
            await _reservationService.CreateAsync(dto);
            return dto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> Update(int id, [FromBody]ReservationDto dto)
        {
            dto.Id = id;
            var existing = await _reservationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Reservation with id {id} not found");
            await _reservationService.UpdateAsync(dto);
            return dto;
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ReservationDto>> Delete(int id)
        {
            var existing = await _reservationService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Reservation with id {id} not found");
            await _reservationService.DeleteAsync(id);
            return NoContent();
        }


    }
}

