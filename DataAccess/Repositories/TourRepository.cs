using System;
using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using Rota.Core.Interfaces;
using Rota.Entities.DTOs;

namespace Rota.DataAccess.Repositories
{
	public class TourRepository : GenericRepository<Tour>, ITourRepository
	{
        private readonly AppDbContext _context;

        public TourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

            public async Task<List<Tour>> GetPopularToursAsync()
            {
            return await _context.Tours
    .Include(t => t.Comments)
    .Where(t => t.Comments.Any())
    .Where(t => t.Comments.Average(c => c.Rating) >= 4)
    .ToListAsync();
        }

        public async Task<IEnumerable<Tour>> SearchAsync(string query)
        {
            return await _context.Tours
                .Where(t => t.Title.Contains(query)).ToListAsync();

           
        }
    }
}

