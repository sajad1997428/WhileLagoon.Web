using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WhileLagoon.Appliction.Common.Interfaces
{
    public interface IRepository< T> where T : class
    {
        IEnumerable<T>GetAll(Expression<Func<T, bool>>? filter=null,string? indcludeProerties=null);
        T Get(Expression<Func<T,bool>>filter,string? indcludeProerties=null);
        bool Any(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Remove(T entity);
    }
}
