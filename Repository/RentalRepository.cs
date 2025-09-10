using RetroVHSRental.Models;
using RetroVHSRental.Data;
using Microsoft.EntityFrameworkCore;

namespace RetroVHSRental.Repository
{
    public class RentalRepository
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
    }
}
