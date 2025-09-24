using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IFilmRepository
    {
        Task<Film> GetByIdAsync(int id);

        // Paginering + sökning + sortering
        Task<(IEnumerable<Film> Films, int TotalCount)> GetPagedAsync(
            string search, string sortOrder, int pageNumber, int pageSize
        );
    }
}
