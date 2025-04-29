using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class SchoolRepository : Repository<School>, ISchoolRepository
    {
        private readonly AppDbContext _context;

        public SchoolRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<School?> GetSchoolsByIdsAsync(int id)
        {
            return await _context.Schools
                .FirstOrDefaultAsync(school => school.SchoolId == id);
        }

    }
}
