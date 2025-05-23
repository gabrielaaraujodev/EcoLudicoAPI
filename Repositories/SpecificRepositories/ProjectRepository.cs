﻿using EcoLudicoAPI.Context;
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

            query = query.Include(p => p.Comments);

            return await query.ToListAsync();
        }


        public async Task<Project> GetByIdAsync(int projectId)
        {
            return await _context.Projects
                .Include(p => p.ImageUrls) 
                .Include(p => p.Comments)  
                    .ThenInclude(c => c.User) 
                .Include(p => p.School)     
                    .ThenInclude(s => s.Teachers) 
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<IEnumerable<Project>> GetProjectsBySchoolIdAsync(int schoolId)
        {
            return await _context.Projects
                .Include(p => p.ImageUrls)
                .Where(p => p.SchoolId == schoolId)
                .ToListAsync();
        }


    }

}
