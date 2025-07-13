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

        public async Task<List<PopularTourDto>> GetPopularToursAsync()
        {
            return await _context.Tours
       .Include(t => t.Comments)
       .Where(t => t.Comments.Any())
       .Select(t => new PopularTourDto
       {
           Id = t.Id,
           Title = t.Title,
           ImageUrl = t.ImageUrl // buradan direkt alınıyor
       })
       .Where(t => _context.Comments
           .Where(c => c.TourId == t.Id)
           .Average(c => c.Rating) >= 4)
       .ToListAsync();
        }
    }
}

