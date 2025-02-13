using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Breed> Breeds { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) => Database.EnsureCreated();
    }
}
