using Microsoft.EntityFrameworkCore;
using RetroVHSRental.Data;
using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _context;

        public FilmRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _context.Films
                .Include(f => f.Language)
                .Include(f => f.FilmCategories)
                    .ThenInclude(fc => fc.Category)
                .Include(f => f.FilmActors)
                    .ThenInclude(fa => fa.Actor)
                .FirstOrDefaultAsync(f => f.FilmId == id);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<(IEnumerable<Film> Films, int TotalCount)> GetPagedAsync(
            string search, string sortOrder, int pageNumber, int pageSize, int? categoryId = null)
        {
            var query = _context.Films
                .Include(f => f.Language)
                .Include(f => f.FilmCategories).ThenInclude(fc => fc.Category)
                .Include(f => f.FilmActors).ThenInclude(fa => fa.Actor)
                .AsQueryable();

            // Filtrado por búsqueda
            if (!string.IsNullOrEmpty(search))
            {
                var searchParts = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(f =>
                    f.Title.Contains(search) ||
                    f.FilmActors.Any(fa => searchParts.All(part =>
                        fa.Actor.FirstName.Contains(part) ||
                        fa.Actor.LastName.Contains(part)
                    ))
                );
            }

            // Filtrado por categoría
            if (categoryId.HasValue)
            {
                query = query.Where(f => f.FilmCategories.Any(fc => fc.CategoryId == categoryId.Value));
            }

            // Ordenamiento
            query = sortOrder switch
            {
                "title_desc" => query.OrderByDescending(f => f.Title),
                "Year" => query.OrderBy(f => f.Release_year),
                "year_desc" => query.OrderByDescending(f => f.Release_year),
                _ => query.OrderBy(f => f.Title)
            };

            var totalCount = await query.CountAsync();

            var films = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (films, totalCount);
        }
    }
}