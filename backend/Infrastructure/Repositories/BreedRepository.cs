using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BreedRepository : IRepository<Breed>
    {
        private readonly ApplicationDbContext _context;

        public BreedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Breeds.AnyAsync(b => b.Id == id);
        }

        public async Task AddAsync(Breed entity)
        {
            await _context.Breeds.AddAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
