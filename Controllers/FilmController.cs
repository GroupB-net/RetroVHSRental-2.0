using Microsoft.AspNetCore.Mvc;
using RetroVHSRental.Models;
using RetroVHSRental.Repository;

namespace RetroVHSRental.Controllers
{
    public class FilmsController : Controller
    {
        private readonly IFilmRepository _filmRepository;

        public FilmsController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        // GET: Films
        public async Task<IActionResult> Index(string searchString, string sortOrder)
        {
            var films = await _filmRepository.GetAllAsync();

            
            if (!string.IsNullOrEmpty(searchString))
            {
                films = films.Where(f => f.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            // Ordnar
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

            films = sortOrder switch
            {
                "title_desc" => films.OrderByDescending(f => f.Title),
                "Year" => films.OrderBy(f => f.Release_year),
                "year_desc" => films.OrderByDescending(f => f.Release_year),
                _ => films.OrderBy(f => f.Title),
            };

            return View(films);
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var film = await _filmRepository.GetByIdAsync(id);
            if (film == null) return NotFound();
            return View(film);
        }
    }
}