using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhileLagoon.Appliction.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IVillaRepsitory Villa {  get; }
        IVillaNumberRepsitory VillaNumber { get; }
        void save();
    }
}
