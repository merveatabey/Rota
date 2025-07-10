using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
	{
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
       
	}
}

