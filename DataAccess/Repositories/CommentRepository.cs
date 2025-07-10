using System;
using DataAccess;
using Entities;
using Rota.Core.Interfaces;

namespace Rota.DataAccess.Repositories
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{

		private readonly AppDbContext _context;

		public CommentRepository(AppDbContext context) : base(context)
		{
			_context = context;
		}
	}
}

