﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Domian.Entities;

namespace WhileLagoon.Appliction.Common.Interfaces
{
    public interface IVillaRepsitory:IRepository<Villa>
    {
      
       
   
        void update(Villa entity);
       
    }
}
