using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EcoLudicoAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        public T Update(T entity)
        {
            throw new NotImplementedException();
        }
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }     
    }
}
