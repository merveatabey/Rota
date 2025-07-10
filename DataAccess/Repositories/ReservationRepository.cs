using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class ReservationRepository  :GenericRepository<Reservation>, IReservationRepository
	{
        private readonly AppDbContext _context;

        public ReservationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

	}
}

