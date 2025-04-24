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

        public async Task<User?> GetUserByEmailAsync(string email)
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


    }
}
