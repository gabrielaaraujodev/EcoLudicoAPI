using EcoLudicoAPI.Context;
using EcoLudicoAPI.Models;
using EcoLudicoAPI.Repositories.SpecificRepositories;

namespace EcoLudicoAPI.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IRepository<User>? _users;
        private IRepository<School>? _schools;
        private IRepository<Project>? _projects;
        private IRepository<FavoriteProject>? _favoriteProjects;
        private IRepository<Comment>? _comments;
        private IRepository<Address>? _addresses;

        //-----------------------------------
        private IProjectRepository? _projectRepo;
        //-----------------------------------

        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Lazy Loading
        public IRepository<User> Users => _users ?? new Repository<User>(_context);
        public IRepository<School> Schools => _schools ?? new Repository<School>(_context);
        public IRepository<Project> Projects => _projects ?? new Repository<Project>(_context);
        public IRepository<FavoriteProject> FavoriteProjects => _favoriteProjects ?? new Repository<FavoriteProject>(_context);
        public IRepository<Comment> Comments => _comments ?? new Repository<Comment>(_context);
        public IRepository<Address> Addresses => _addresses ?? new Repository<Address>(_context);

        //-----------------------------------
        public IProjectRepository ProjectRepository => _projectRepo ?? new ProjectRepository(_context);
        //-----------------------------------

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
