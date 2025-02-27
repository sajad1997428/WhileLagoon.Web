﻿using Microsoft.EntityFrameworkCore;
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
    public class VillaRepsitory :Repository<Villa>, IVillaRepsitory
    {
        private readonly CLSDbContext db;
        public VillaRepsitory(CLSDbContext db):base(db)
        {
            this.db = db;
        }
        public void Add(Villa entity)
        {
            db.Add(entity);
        }
       

        public void update(Villa entity)
        {
            db.Update(entity);
        }
    }
}
