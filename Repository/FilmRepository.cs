using RetroVHSRental.Data;
using RetroVHSRental.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id);
        }

        public async Task<(IEnumerable<Film> Films, int TotalCount)> GetPagedAsync(
            string search, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.Films.AsQueryable();

            // Filtrering
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(f => f.Title.Contains(search));
            }

            // Sortering
            query = sortOrder switch
            {
                "title_desc" => query.OrderByDescending(f => f.Title),
                "Year" => query.OrderBy(f => f.Release_year),
                "year_desc" => query.OrderByDescending(f => f.Release_year),
                _ => query.OrderBy(f => f.Title),
            };

            // Antal före pagination
            var totalCount = await query.CountAsync();

            // Pagination
            var films = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (films, totalCount);
        }
    }
}

