using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public interface IFilmRepository
    {
        Task<Film> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        /// <summary>
        /// Devuelve una página de films filtrada por búsqueda, orden y categoría.
        /// </summary>
        /// <param name="search">Texto de búsqueda (titulo o actor)</param>
        /// <param name="sortOrder">Orden: "title_desc", "Year", "year_desc"</param>
        /// <param name="pageNumber">Número de página</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <param name="categoryId">Filtrar por categoría opcional</param>
        Task<(IEnumerable<Film> Films, int TotalCount)> GetPagedAsync(
            string search, string sortOrder, int pageNumber, int pageSize, int? categoryId = null);

    }
}
