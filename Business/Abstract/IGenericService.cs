using Core.Others;
using System.Linq.Expressions;

namespace Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        CoreResponse<T> Get(Expression<Func<T, bool>> filter, string includeProperties = "", int userId = default(int));
        CoreResponse<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int userId = default(int));
        CoreResponse<T> Add(T entity, int userId = default(int));
        CoreResponse<T> Update(T entity, int userId = default(int));
        CoreResponse<T> Delete(T entity, int userId = default(int));
        CoreResponse<T> DeleteById(int id, int userId = default(int));
        Task<CoreResponse<T>> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = "", int userId = default(int));
        Task<CoreResponse<IEnumerable<T>>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int userId = default(int));
        Task<CoreResponse<T>> AddAsync(T entity, int userId = default(int));
        Task<CoreResponse<T>> DeleteByIdAsync(int id, int userId = default(int));
    }
}
