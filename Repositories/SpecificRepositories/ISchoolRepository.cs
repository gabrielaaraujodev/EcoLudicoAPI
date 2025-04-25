using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<List<School>> GetSchoolsByIdsAsync(List<int> ids);
    }
}
