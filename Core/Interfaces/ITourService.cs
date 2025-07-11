using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ITourService : IGenericService<TourDto>
	{
		Task<TourDetailsDto> GetTourDetailsAsync(int tourId);
	}
}

