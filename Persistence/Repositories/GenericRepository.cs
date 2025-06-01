using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(CaffeDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly DbSet<TEntity> _dbSet = _dbContext.Set<TEntity>();
        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() =>
            await _dbContext.Set<TEntity>()
                            .AsNoTracking()
                            .ToListAsync();




        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);




        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (include is not null)
            {
                var includedQuery = include(query);
                query = includedQuery ?? query;
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithFilterAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            // Start with AsNoTracking to disable lazy loading and entity tracking
            IQueryable<TEntity> query = _dbSet.AsNoTracking().Where(predicate);

            if (include is not null)
            {
                var includedQuery = include(query);
                query = includedQuery ?? query;
            }

            // Use AsSplitQuery for better performance with complex includes
            return await query.AsSplitQuery().ToListAsync();
        }

        public async Task<TEntity?> GetFirstOrDefaultAsync(
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (include is not null)
                query = include(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

    }
}
