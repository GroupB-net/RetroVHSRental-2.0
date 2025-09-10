using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(int id);
    }
}
