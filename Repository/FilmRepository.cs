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
        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context.Films
                .Include(f => f.Language)
            .Include(f => f.FilmCategories)
                .ThenInclude(fc => fc.Category)
            .Include(f => f.FilmActors)
                .ThenInclude(fa => fa.Actor)
                .ToListAsync(); 
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _context.Films.FirstOrDefaultAsync(f => f.FilmId == id);
        }

        //Pagination
        public async Task<IEnumerable<Film>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Films
                .OrderBy(f => f.FilmId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Films.CountAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
