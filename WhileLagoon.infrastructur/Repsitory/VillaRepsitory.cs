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
    public class VillaRepsitory : IVillaRepsitory
    {
        private readonly CLSDbContext db;
        public VillaRepsitory(CLSDbContext db)
        {
            this.db = db;
        }
        public void Add(Villa entity)
        {
            db.Add(entity);
        }

        public Villa Get(Expression<Func<Villa, bool>> fillter = null, string? indcludeProerties = null)
        {
            IQueryable<Villa> query = db.Set<Villa>();
            if (fillter != null)
            {
                query = query.Where(fillter);
            }
            if (!string.IsNullOrWhiteSpace(indcludeProerties))
            {
                foreach (var includeProp in indcludeProerties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<Villa> GetAll(Expression<Func<Villa, bool>>? fillter = null, 
            string? indcludeProerties = null)
        {
            IQueryable<Villa>query=db.Set<Villa>();
            if(fillter != null)
            {
                query = query.Where(fillter);
            }
            if (!string.IsNullOrWhiteSpace(indcludeProerties))
            {
                foreach(var includeProp in indcludeProerties.Split(new char[] {','},
                    StringSplitOptions.RemoveEmptyEntries))
                {
                     query=query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public void Remove(Villa entity)
        {
           db.Remove(entity);
        }

        public void save()
        {
           db.SaveChanges();
        }

        public void update(Villa entity)
        {
            db.Update(entity);
        }
    }
}
