using System;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ITourRepository 
	{
		Task<List<PopularTourDto>> GetPopularToursAsync();
	}
}

