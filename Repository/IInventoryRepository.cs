using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> GetByIdAsync(int id);
        Task AddAsync(Inventory inventory);
        Task UpdateAsync(Inventory inventory);
        Task RemoveAsync(Inventory inventory);
    }
}
