using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;

namespace NBB.Api.services
{
    public class EnterpriseDbContext : DbContext
    {
       public EnterpriseDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .HasKey(e => e.EnterpriseNumber);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Enterprise> Enterprise { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<FinancialData> financialData { get; set; }
    }
}
