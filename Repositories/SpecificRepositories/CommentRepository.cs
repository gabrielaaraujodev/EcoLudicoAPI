using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User?> GetByIdWithCommentsAsync(int id)
        {
            return await _context.Users
                .Include(u => u.MadeComments)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

    }
}
