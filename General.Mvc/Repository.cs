using General.Core.data;
using General.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace General.Mvc
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        private GeneralDbContext _generalDbContext;

        public Repository(GeneralDbContext generalDbContext)
        {
            this._generalDbContext = generalDbContext;
        }
        
        public DbContext dbContext
        {
            get { return _generalDbContext; }
        }

        public DbSet<TEntity> Entities
        {
            get { return dbContext.Set<TEntity>(); }
        }

        public IQueryable<TEntity> Table
        {
            get { return Entities; }
        }

        public List<TEntity> GetAll()
        {
            return Entities.ToList();
        }
    }
}
