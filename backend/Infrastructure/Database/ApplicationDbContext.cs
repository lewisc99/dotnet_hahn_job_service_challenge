using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Breed> Breeds { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breed>(entity =>
            {
                entity.ToTable("Breeds");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }

    }
}
