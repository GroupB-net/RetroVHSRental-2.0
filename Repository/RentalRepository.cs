using RetroVHSRental.Models;
using RetroVHSRental.Data;
using Microsoft.EntityFrameworkCore;

namespace RetroVHSRental.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly ApplicationDbContext context;

        public RentalRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await context.Rentals.ToListAsync();
        }
        public async Task<Rental> GetByIdAsync(int id)
        {
            return await context.Rentals.FirstOrDefaultAsync(b => b.RentalId == id);
        }
        public async Task AddAsync(Rental rental)
        {
            context.Rentals.Add(rental);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Rental rental)
        {
            context.Rentals.Update(rental);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Rental rental)
        {
            context.Rentals.Remove(rental);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rental>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await context.Rentals
                .Include(b => b.Customer)
                .OrderBy(r => r.RentalId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await context.Rentals.CountAsync();
        }
    }
}
