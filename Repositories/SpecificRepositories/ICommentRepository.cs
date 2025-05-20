using EcoLudicoAPI.Models;
using System.Threading.Tasks;

namespace EcoLudicoAPI.Repositories.SpecificRepositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> GetCommentByIdWithProjectAndSchoolInfoAsync(int commentId);
    }
}