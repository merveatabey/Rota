using System;
using AutoMapper;
using Entities;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.Business.Services
{
	public class TourActivityService : ITourActivityService
	{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

		public TourActivityService(IUnitOfWork unitOfWork, IMapper mapper)
		{
            _unitOfWork = unitOfWork;
            _mapper = mapper;
		}

        public async Task CreateAsync(TourActivityDto dto)
        {
            var tourActivity = _mapper.Map<TourActivity>(dto);
            await _unitOfWork.TourActivities.AddAsync(tourActivity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tourActivitty = await _unitOfWork.TourActivities.GetByIdAsync(id);

            if(tourActivitty == null)
            {
                throw new Exception("TourActivity not found");
            }
            _unitOfWork.TourActivities.Delete(tourActivitty);
            await _unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TourActivityDto>> GetAllAsync()
        {
            var tourActivities = await _unitOfWork.TourActivities.GetAllAsync();
            return _mapper.Map<IEnumerable<TourActivityDto>>(tourActivities);
        }

        public Task<TourActivityDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TourActivityDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

