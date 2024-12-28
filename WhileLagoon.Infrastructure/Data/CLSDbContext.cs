using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Domain.Entities;

namespace WhileLagoon.Infrastructure.Data
{
    public class CLSDbContext:DbContext
    {
       public CLSDbContext(DbContextOptions<CLSDbContext> options) : base(options) 
        {

        }
        public DbSet<Villa> Villas { get; set; }
    }
}
