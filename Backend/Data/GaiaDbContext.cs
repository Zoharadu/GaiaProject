using Common;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class GaiaDbContext: DbContext
    {
        public GaiaDbContext(DbContextOptions<GaiaDbContext> options)
           : base(options)
        {

        }

        public DbSet<OperationRequest> GaiaDB { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OperationRequest>().ToTable("GaiaDB");
        //}
    }
}

