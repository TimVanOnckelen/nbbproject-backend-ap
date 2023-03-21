using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;

namespace NBB.Api.services
{
    public class EnterpriseDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Enterprise");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Enterprise> Enterprise { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
