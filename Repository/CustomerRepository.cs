using RetroVHSRental.Models;
using RetroVHSRental.Data;
using Microsoft.EntityFrameworkCore;

namespace RetroVHSRental.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext context;

        public CustomerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            return await context.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }
    }
}
