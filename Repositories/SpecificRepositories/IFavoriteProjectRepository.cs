using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface IFavoriteProjectRepository : IRepository<FavoriteProject>
    {
        Task<List<Project>> GetByIdsAsync(List<int> projectIds);
    }
}
