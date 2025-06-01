using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<TEntity?> GetFirstOrDefaultAsync(
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IQueryable<TEntity>>? include = null
);

        Task<IEnumerable<TEntity>> GetAllWithFilterAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);



    }
}
