using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetProjectsByAgeRangeAsync(AgeRange? ageRange);

        Task<Project> GetByIdAsync(int projectId);
    }
}
