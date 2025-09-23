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
            return await context.Rentals.OrderByDescending(r => r.RentalDate).ToListAsync();
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
                .OrderByDescending(r => r.RentalDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> CountAsync()
        {
            return await context.Rentals.CountAsync();
        }

        public async Task<IEnumerable<Rental>> RentalsExpiringToday()
        {
            var todaysDate = DateTime.Today; //skapar en variabel för dagens datum
            var tomorrow = todaysDate.AddDays(1);

            return await context.Rentals.Where(r => r.ReturnDate >= todaysDate && r.ReturnDate < tomorrow).ToListAsync();

            
        }
    }
}
