using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class FavoriteTourRepository : GenericRepository<FavoriteTour>, IFavoriteTourRepository
	{
        private readonly AppDbContext _context;

        public FavoriteTourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
	}
}

