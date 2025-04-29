using System.Linq.Expressions;
using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(user => user.Address)
                .Include(user => user.School)!
                    .ThenInclude(school => school.Address)
                .Include(user => user.FavoriteSchools)!
                    .ThenInclude(favoriteSchools => favoriteSchools.Address)
                .Include(user => user.FavoriteProjects)!
                    .ThenInclude(user => user.Projeto)!
                        .ThenInclude(user => user.School)
                .Include(user => user.FavoriteProjects)!
                    .ThenInclude(user => user.Projeto)!
                        .ThenInclude(user => user.Comments)
                .Include(user => user.MadeComments)
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Address)
                .Include(u => u.School)
                    .ThenInclude(s => s.Address)
                .Include(u => u.FavoriteSchools)
                .Include(u => u.FavoriteProjects)
                .Include(u => u.MadeComments)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByIdWithFavoriteSchoolsAsync(int id)
        {
            return await _context.Users
                .Include(u => u.FavoriteSchools)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User?> GetByIdWithFavoriteProjectsAsync(int id)
        {
            return await _context.Users
                .Include(u => u.FavoriteProjects)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }
        public async Task<IEnumerable<User>> GetAllUsersWithDetailsAsync()
        {
            return await _context.Users
                .Include(u => u.Address)
                .Include(u => u.School)
                    .ThenInclude(s => s.Address)
                .Include(u => u.FavoriteProjects)
                .Include(u => u.FavoriteSchools)
                .Include(u => u.MadeComments)
                .ToListAsync();
        }

    }
}
