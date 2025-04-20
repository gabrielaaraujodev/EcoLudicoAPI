using System.Linq.Expressions;
using System.Runtime.Intrinsics.X86;

namespace EcoLudicoAPI.Repository
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        // " Expression<Func<T, bool>> predicate " -> Signfica que o método pode receber uma função ' lambda ' como parâmetro.
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
