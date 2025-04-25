using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.SpecificRepositories;

namespace EcoLudicoAPI.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<School> Schools { get; }
        IRepository<Project> Projects { get; }
        IRepository<FavoriteProject> FavoriteProjects { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Address> Addresses { get; }

        //----------------------------------
        IProjectRepository ProjectRepository { get; }

        Task CommitAsync();
        void Dispose();
    }
}
