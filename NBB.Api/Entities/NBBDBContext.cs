using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NBB.Api.Models;

namespace NBB.Api.Entities
{
    public class NBBDBContext: DbContext
    {
        public NBBDBContext(DbContextOptions<NBBDBContext> options) : base(options) { }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<FinancialData> FinancialData { get; set;}
        public DbSet<Address> Address { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enterprise>()
                .HasKey(e => e.EnterpriseNumber);
            base.OnModelCreating(modelBuilder);
        }
    }
}
