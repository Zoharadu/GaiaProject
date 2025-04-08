using Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data
{
    public class GaiaDbContext : DbContext
    {
        public GaiaDbContext(DbContextOptions<GaiaDbContext> options)
           : base(options)
        {
        }

        public DbSet<OperationRequest> Operations => Set<OperationRequest>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OperationRequest>().ToTable("Operations");
        }
    }
}

