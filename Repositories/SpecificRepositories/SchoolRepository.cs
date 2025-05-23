﻿using EcoLudicoAPI.Context;
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
                .Include(s => s.Address)
                .Include(s => s.Teachers)
                .Include(s => s.Projects)
                .Include(s => s.UsersWhoFavorited)
                .FirstOrDefaultAsync(s => s.SchoolId == id);
        }

    }
}
