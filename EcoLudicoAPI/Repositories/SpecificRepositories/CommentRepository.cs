using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments
                .Include(c => c.User) 
                .FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task<Comment?> GetCommentByIdWithProjectAndSchoolInfoAsync(int commentId)
        {
            return await _context.Comments
                .Include(c => c.User) 
                .Include(c => c.Project) 
                    .ThenInclude(p => p.School) 
                        .ThenInclude(s => s.Teachers) 
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }
    }
}