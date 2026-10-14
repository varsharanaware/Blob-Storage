using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFun.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Models.SalesRequestFunctionModel> SalesRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //USED TO SET PRIMARY KEY FOR THE MODEL. IF NOT SET, EF CORE WILL USE CONVENTION TO SET PRIMARY KEY
            modelBuilder.Entity<Models.SalesRequestFunctionModel>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
