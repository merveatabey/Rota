using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class ReservationService : IReservationService
	{
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
		public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfwork = unitOfWork;
            _mapper = mapper;
		}

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _unitOfwork.Reservations.GetAllAsync();
            return _mapper.Map<IEnumerable<ReservationDto>>(reservations);
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _unitOfwork.Reservations.GetByIdAsync(id);
            if(reservation == null)
            {
                throw new Exception("reservation not found");
            }
            return _mapper.Map<ReservationDto>(reservation);
        }

        public async Task CreateAsync(ReservationDto dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);
            await _unitOfwork.Reservations.AddAsync(reservation);
            await _unitOfwork.SaveAsync();
        }

        public async Task UpdateAsync(ReservationDto dto)
        {
            var reservation = await _unitOfwork.Reservations.GetByIdAsync(dto.Id);
            if(reservation == null)
            {
                throw new Exception("reservation not found");
            }
            _mapper.Map(dto, reservation);

            _unitOfwork.Reservations.Update(reservation);
            await _unitOfwork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await _unitOfwork.Reservations.GetByIdAsync(id);
            if (reservation == null)
            {
                throw new Exception("reservation not found");
            }
            _unitOfwork.Reservations.Delete(reservation);
            await _unitOfwork.SaveAsync();

        }
    }
}

