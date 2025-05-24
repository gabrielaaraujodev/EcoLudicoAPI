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
        IUserRepository UserRepository { get; }
        ISchoolRepository SchoolRepository { get; }
        IFavoriteProjectRepository FavoriteProjectRepository { get; }
        ICommentRepository CommentRepository { get; }

        Task CommitAsync();
    }
}
