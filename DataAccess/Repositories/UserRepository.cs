using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
        private AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

