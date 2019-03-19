using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Core.data
{
    public interface IRepository<TEntity> where TEntity:class
    {

        DbContext dbContext { get; }

        DbSet<TEntity> Entities { get; }

        IQueryable<TEntity> Table { get; }


        List<TEntity> GetAll();
    }
}
