using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IRentalRepository
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task<Rental> GetByIdAsync(int id);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
        Task RemoveAsync(Rental rental);

        Task<IEnumerable<Rental>> GetPagedAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
    }
}
