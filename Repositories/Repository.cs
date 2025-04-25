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

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, object>> address = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (address != null)
            {
                query = query.Include(address); 
            }

            return await query.ToListAsync();
        }


        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        // Não irei utilizar mais o "SaveChanges()" diretamente aqui, pois quem estará encarregado seria o 'UoW'.
        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            //_context.SaveChanges();
            return entity;
        }
        public T Update(T entity)
        {
            _context.Set<T>().Update(entity);
            //_context.SaveChanges();
            return entity;
        }
        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            //_context.SaveChanges();
            return entity;
        }     
    }
}
