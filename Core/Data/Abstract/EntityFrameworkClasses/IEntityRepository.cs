using System.Linq.Expressions;

namespace Core.Data.Abstract.EntityFrameworkClasses
{
    public interface IEntityRepository<T> where T : class, new()
    {
        T FindById(int id);
        T Get(Expression<Func<T, bool>> filter, string includeProperties = "");
        IEnumerable<T> GetList(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        bool Any(Expression<Func<T, bool>> filter);
        int Save();

        Task<T> FindByIdAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string includeProperties = "");
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task AddAsync(T entity);
        Task<int> SaveAsync();
    }
}
