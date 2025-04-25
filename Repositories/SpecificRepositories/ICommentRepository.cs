using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<User?> GetByIdWithCommentsAsync(int id);
    }
}
