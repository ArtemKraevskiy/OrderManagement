using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Base;
using OrderManagement.Data;
using OrderManagement.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace OrderManagement.Repositories.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await _dbSet.Where(_ => !_.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(_ => !_.IsDeleted).Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(_ => !_.IsDeleted).Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var entity = await _dbSet.AddAsync(item);
            return entity.Entity;
        }
        public TEntity Update(TEntity item)
        {
            return _dbSet.Update(item).Entity;
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
        }

        public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _dbSet.Where(predicate).ToListAsync();
            entities.Select(_ => _.IsDeleted = true);
            _dbSet.UpdateRange(entities);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetOneWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return await includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty))
                .Where(entity => !entity.IsDeleted).Where(predicate).FirstOrDefaultAsync();
        }
    }
}
