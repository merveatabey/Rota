using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class TourRepository : GenericRepository<Tour>, ITourRepository
	{
        private readonly AppDbContext _context;

        public TourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        
	}
}

