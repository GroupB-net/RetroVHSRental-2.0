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
        public async Task<IActionResult> Index(string searchString, string sortOrder, int page = 1)
        {
            int pageSize = 10; // filmer per sida

            var (films, totalCount) = await _filmRepository.GetPagedAsync(searchString, sortOrder, page, pageSize);

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // Skicka sortering till View
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

            // Skicka pagination info
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.SearchString = searchString;
            ViewBag.SortOrder = sortOrder;

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