using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IFilmRepository
    {
        Task<Film>GetByIdAsync(int id);
        Task<IEnumerable<Film>> GetAllAsync();

    }
}
