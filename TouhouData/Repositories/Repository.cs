using System.Collections.Generic;
using System;
using System.Linq;
using TouhouArticleMaker.Domain.Repositories.Interfaces;
using TouhouData;
using Microsoft.EntityFrameworkCore;

namespace TouhouData.Repositories
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        protected DbContext _dbContext;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TEntity obj)
        {
            _dbContext.Set<TEntity>().Add(obj);
            _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            _dbContext.Set<TEntity>().Remove(obj);
            _dbContext.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}