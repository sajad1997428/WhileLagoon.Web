using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.Domian.Entities;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.infrastructur.Repsitory
{
    public class Repository<T> : IRepository<T>where T : class
    {
        private readonly CLSDbContext db;
        internal DbSet<T> dbset;
        public Repository(CLSDbContext db)
        {
            this.db = db;
            dbset=db.Set<T>();
        }

        public bool Any(Expression<Func<T, bool>> filter)
        {
           return dbset.Any(filter);
        }

        void IRepository<T>.Add(T entity)
        {
            dbset.Add(entity);
        }

        T IRepository<T>.Get(Expression<Func<T, bool>> filter, string? indcludeProerties)
        {
            IQueryable<T> query = dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(indcludeProerties))
            {
                foreach(var includePrp in indcludeProerties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(includePrp);
                }
               
            }
             return query.FirstOrDefault();
        }

        IEnumerable<T> IRepository<T>.GetAll(Expression<Func<T, bool>>? filter, string? indcludeProerties)
        {
            IQueryable<T> query = dbset;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            if(!string.IsNullOrEmpty( indcludeProerties))
            {
                foreach(var includePrp in indcludeProerties
                 .Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries))
                {
                  query=  query.Include(includePrp);
                }
            }
            return query.ToList();
        }

        void IRepository<T>.Remove(T entity)
        {
            dbset.Remove(entity);
        }
    }
}
