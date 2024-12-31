using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Appliction.Common.Interfaces;
using WhileLagoon.infrastructur.Data;

namespace WhileLagoon.infrastructur.Repsitory
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CLSDbContext db;
        public IVillaRepsitory Villa { get;private set; }
        public IVillaNumberRepsitory VillaNumber { get; private set; }
        public UnitOfWork(CLSDbContext db)
        {
            this.db = db;
            Villa=new VillaRepsitory(db);
            VillaNumber=new VillaNumberRepsitory(db);
        }

        public void save()
        {
            db.SaveChanges();
        }
    }
}
