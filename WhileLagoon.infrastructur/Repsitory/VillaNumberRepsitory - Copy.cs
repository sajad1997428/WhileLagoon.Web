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
    public class VillaNumberRepsitory :Repository<VillaNumber>, IVillaNumberRepsitory
    {
        private readonly CLSDbContext db;
        public VillaNumberRepsitory(CLSDbContext db):base(db)
        {
            this.db = db;
        }
       
       

        public void update(VillaNumber entity)
        {
            db.Update(entity);
        }
    }
}
