using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IFilmRepository
    {
        Task<Film>GetByIdAsync(int id);
        Task<IEnumerable<Film>> GetAllAsync();
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        //Pagination
        Task<IEnumerable<Film>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();

    }
}
