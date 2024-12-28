using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhileLagoon.Domian.Entities;

namespace WhileLagoon.infrastructur.Data
{
    public class CLSDbContext:DbContext
    {
        public CLSDbContext(DbContextOptions<CLSDbContext> options) : base(options) 
        {
            
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber>VillaNubmers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name="Royal Villa",
                Description="Fusce 11 tincidunt maximus leo ,sed scelerisque massa auctor sit",
                ImageUel= "https://licensed-image - Copy",
                Occupancy=4,
                price=200,
                sqft=100,
                
            },
             new Villa
             {
                 Id = 2,
                 Name = "Royalk Villa",
                 Description = "Fusce 11 tincidunt maximus leo ,sed scelerisque massa auctor sit",
                 ImageUel = "https://images",
                 Occupancy = 4,
                 price = 202,
                 sqft = 103,

             }
            );
            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                { 
                   Villa_Number = 101,
                   VillaId=1,
                },
                new VillaNumber
                {
                    Villa_Number = 103,
                    VillaId = 1,
                },
                new VillaNumber
                {
                    Villa_Number = 204,
                    VillaId = 2,
                },
                new VillaNumber
                {
                    Villa_Number = 205,
                    VillaId = 2,
                }

                );
        }
    }
}
