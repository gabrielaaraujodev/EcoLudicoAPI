using EcoLudicoAPI.Context; 
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class FavoriteProjectRepository : Repository<FavoriteProject>, IFavoriteProjectRepository
    {
        private readonly AppDbContext _context; 

        public FavoriteProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<FavoriteProject?> GetByUserAndProjectAsync(int userId, int projectId)
        {
            return await _context.FavoriteProjects
                                 .FirstOrDefaultAsync(fp => fp.UserId == userId && fp.ProjectId == projectId);
        }
        public async Task<IEnumerable<Project>> GetFavoriteProjectsByUserIdAsync(int userId)
        {
            return await _context.FavoriteProjects
                                 .Where(fp => fp.UserId == userId)
                                 .Select(fp => fp.Projeto) 
                                 .Include(p => p.ImageUrls) 
                                 .Include(p => p.School) 
                                    .ThenInclude(s => s.Address) 
                                 .Include(p => p.Comments) 
                                    .ThenInclude(c => c.User) 
                                 .ToListAsync();
        }
        public async Task<List<Project>> GetByIdsAsync(List<int> projectIds)
        {
            return await _context.Projects
                .Where(p => projectIds.Contains(p.ProjectId))
                .Include(p => p.ImageUrls)
                .Include(p => p.Comments)
                .Include(p => p.School)
                    .ThenInclude(s => s.Address)
                .ToListAsync();
        }
    }
}