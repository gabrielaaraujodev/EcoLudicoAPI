using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.SpecificRepositories;

namespace EcoLudicoAPI.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<User>? _users;
        private IRepository<School>? _schools;
        private IRepository<Project>? _projects;
        private IRepository<FavoriteProject>? _favoriteProjects;
        private IRepository<Comment>? _comments;
        private IRepository<Address>? _addresses;

        private IProjectRepository? _projectRepo;
        private IUserRepository? _userRepo;
        private ISchoolRepository? _schoolRepo;
        private IFavoriteProjectRepository? _favoriteProjectRepo;
        private ICommentRepository? _commentRepo;

        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<User> Users => _users ?? new Repository<User>(_context);
        public IRepository<School> Schools => _schools ?? new Repository<School>(_context);
        public IRepository<Project> Projects => _projects ?? new Repository<Project>(_context);
        public IRepository<FavoriteProject> FavoriteProjects => _favoriteProjects ?? new Repository<FavoriteProject>(_context);
        public IRepository<Comment> Comments => _comments ?? new Repository<Comment>(_context);
        public IRepository<Address> Addresses => _addresses ?? new Repository<Address>(_context);

        public IProjectRepository ProjectRepository => _projectRepo ?? new ProjectRepository(_context);
        public IUserRepository UserRepository => _userRepo ?? new UserRepository(_context);
        public ISchoolRepository SchoolRepository => _schoolRepo ?? new SchoolRepository(_context);
        public IFavoriteProjectRepository FavoriteProjectRepository => _favoriteProjectRepo ?? new FavoriteProjectRepository(_context);
        public ICommentRepository CommentRepository => _commentRepo ?? new CommentRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

    }

}
