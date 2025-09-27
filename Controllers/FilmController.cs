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
        public async Task<IActionResult> Index(string searchString, string sortOrder, int page = 1, int? categoryId = null)
        {
            int pageSize = 10; // filmer per sida

            var (films, totalCount) = await _filmRepository.GetPagedAsync(
                searchString, sortOrder, page, pageSize, categoryId);

            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            ViewBag.CurrentPage = page;

            ViewBag.SearchString = searchString;
            ViewBag.SortOrder = sortOrder;

            var categories = await _filmRepository.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

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