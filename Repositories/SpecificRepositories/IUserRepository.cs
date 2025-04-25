using System.Linq.Expressions;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByIdAsync(int id, Expression<Func<User, object>> include = null);
        Task<User?> GetByIdWithFavoriteSchoolsAsync(int id);
        Task<User?> GetByIdWithFavoriteProjectsAsync(int id);
    }
}
