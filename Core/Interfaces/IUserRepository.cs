using System;
using Entities;

namespace Rota.Core.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
	{
		Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllGuidesAsync();
    }
}

