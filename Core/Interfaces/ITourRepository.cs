using System;
using Entities;
using Rota.Entities.DTOs;

namespace Rota.Core.Interfaces
{
	public interface ITourRepository 
	{
		Task<List<Tour>> GetPopularToursAsync();
		Task<IEnumerable<Tour>> SearchAsync(string query);
	}
}

