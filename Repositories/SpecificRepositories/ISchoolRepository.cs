using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface ISchoolRepository : IRepository<School>
    {
        Task<School?> GetSchoolsByIdsAsync(int id);
    }
}
