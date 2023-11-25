using OrderManagement.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Repositories.Abstractions
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity item);
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> AllAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        void Remove(TEntity item);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Update(TEntity item);
        Task CommitAsync();
        Task<TEntity> GetOneWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
