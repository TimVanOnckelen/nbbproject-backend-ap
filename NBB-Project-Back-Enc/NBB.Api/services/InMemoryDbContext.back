using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;

namespace NBB.Api.services
{
    public class InMemoryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "Enterprise");
        }
        public DbSet<Enterprise> Enterprise { get; set; }
    }
}
