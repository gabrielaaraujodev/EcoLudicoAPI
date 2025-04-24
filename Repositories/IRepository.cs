using System.Linq.Expressions;

namespace EcoLudicoAPI.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        // " Expression<Func<T, bool>> predicate " -> Signfica que o método pode receber uma função ' lambda ' como parâmetro.
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
