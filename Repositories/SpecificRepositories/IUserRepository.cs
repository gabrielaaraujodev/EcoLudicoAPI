using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
    }
}
