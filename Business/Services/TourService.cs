using System;
using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;


namespace Rota.Business.Services
{
	public class TourService : ITourService
	{

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
		public TourService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
		}

        public async Task CreateAsync(TourDto dto)
        {
            var tours = _mapper.Map<Tour>(dto);
            await _unitOfWork.Tours.AddAsync(tours);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tour = await _unitOfWork.Tours.GetByIdAsync(id);
            if(tour == null)
            {
                throw new Exception("Tour not found");
            }

            _unitOfWork.Tours.Delete(tour);
            _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<TourDto>> GetAllAsync()
        {
            var tours = await _unitOfWork.Tours.GetAllAsync();
            return _mapper.Map<IEnumerable<TourDto>>(tours);
        }

        public async Task<TourDto> GetByIdAsync(int id)
        {
            var tour = await _unitOfWork.Tours.GetByIdAsync(id);
            if (tour == null)
            {
                throw new Exception("Tour not found");
            }

            return _mapper.Map<TourDto>(tour);
        }

        // Tour detaylarını ilişkili tüm verilerle birlikte getir
        public async Task<TourDetailsDto> GetTourDetailsAsync(int tourId)
        {
            var tour = await _unitOfWork.Tours.Query()
                .Include(t => t.Guide)
                .Include(t => t.Days)
                    .ThenInclude(d => d.Activities)
                .Include(t => t.Hotels)
                .FirstOrDefaultAsync(t => t.Id == tourId);

            if (tour == null) throw new KeyNotFoundException("Tour not found.");

            return _mapper.Map<TourDetailsDto>(tour);
        }

        public async Task UpdateAsync(TourDto dto)
        {
            var tour = await _unitOfWork.Tours.GetByIdAsync(dto.Id);
            if(tour == null)
            {
                throw new Exception("Tour not found");
            }

            //Dto'daki güncel bilgileri Entity'e aktar
            _mapper.Map(dto, tour);
            _unitOfWork.Tours.Update(tour);
            await _unitOfWork.SaveAsync();
        }
    }
}

