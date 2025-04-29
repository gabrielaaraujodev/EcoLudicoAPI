using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment?> GetByIdAsync(int id);
    }
}
