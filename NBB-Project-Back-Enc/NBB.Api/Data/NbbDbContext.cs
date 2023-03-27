using Microsoft.EntityFrameworkCore;

namespace NBB.Api.Data
{
    public class NbbDbContext<T> : DbContext where T : class
    {
        /// <summary>
        /// Communicates with the database
        /// </summary>
        /// <param name="options"></param>
        public NbbDbContext(DbContextOptions<NbbDbContext<T>> options) : base(options)
        {
        }
        //TODO: Add DbSet for each entity or keep this generic?
        public DbSet<T> Entities { get; set; }

    }
}
