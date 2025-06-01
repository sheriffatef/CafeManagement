using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UnitOfWork(CaffeDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repository = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var reponame = typeof(TEntity).Name;
            if (_repository.ContainsKey(reponame))
            {
                return (IGenericRepository<TEntity, TKey>)_repository[reponame];
            }
            else
            {
                var repository = new GenericRepository<TEntity, TKey>(_dbContext);
                _repository.Add(reponame, repository);
                return repository;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
        public Microsoft.EntityFrameworkCore.DbContext GetDbContext() => _dbContext;



    }
}
