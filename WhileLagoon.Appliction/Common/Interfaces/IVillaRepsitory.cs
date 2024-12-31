using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Domian.Entities;

namespace WhileLagoon.Appliction.Common.Interfaces
{
    public interface IVillaRepsitory
    {
        IEnumerable<Villa>GetAll(Expression<Func<Villa, bool>>? fillter=null,string? indcludeProerties=null);
        public Villa Get(Expression<Func<Villa, bool>> fillter = null, string? indcludeProerties = null);
        void Add(Villa entity);
        void Remove(Villa entity);
        void update(Villa entity);
        void save();
    }
}
