using EcoLudicoAPI.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface IFavoriteProjectRepository : IRepository<FavoriteProject>
    {
        Task<FavoriteProject?> GetByUserAndProjectAsync(int userId, int projectId); 

        Task<IEnumerable<Project>> GetFavoriteProjectsByUserIdAsync(int userId);

    }
}