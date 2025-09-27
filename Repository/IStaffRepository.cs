using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IStaffRepository
    {
        Task<IEnumerable<Staff>> GetAllAsync();
        Task<Staff> GetByIdAsync(byte id);
        Task AddAsync(Staff staff);
        Task RemoveAsync(Staff staff);
        Task UpdateAsync(Staff staff);
    }
}
