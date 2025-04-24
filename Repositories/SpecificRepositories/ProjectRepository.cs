using EcoLudicoAPI.Context;
using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsByAgeRangeAsync(AgeRange? ageRange)
        {
            var query = _context.Projects.AsQueryable();

            if (ageRange.HasValue)
            {
                query = query.Where(p => p.AgeRange == ageRange.Value);
            }

            return await query.ToListAsync();
        }
    }

}
