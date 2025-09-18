using Microsoft.EntityFrameworkCore;
using RetroVHSRental.Data;
using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext context;

        public InventoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Inventory inventory)
        {
            context.Inventory.Add(inventory);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Inventory inventory)
        {
            context.Inventory.Remove(inventory);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await context.Inventory.ToListAsync();
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            return await context.Inventory.FirstOrDefaultAsync(i => i.InventoryId == id);
        }

        public async Task UpdateAsync(Inventory inventory)
        {
            context.Inventory.Update(inventory);
            await context.SaveChangesAsync();
        }
    }
}
