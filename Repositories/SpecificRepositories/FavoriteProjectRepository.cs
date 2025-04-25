using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class FavoriteProjectRepository : Repository<FavoriteProject>, IFavoriteProjectRepository
    {
        private readonly AppDbContext _context;

        public FavoriteProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetByIdsAsync(List<int> projectIds)
        {
            return await _context.Projects
                .Where(p => projectIds.Contains(p.ProjectId))
                .ToListAsync();
        }

    }
}
