using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            int pageSize = 10; //filmer per sida
            IEnumerable<Film> films;

            films = await _filmRepository.GetAllAsync();

            // Kontrollerar om en category-button valts.
            if (categoryId.HasValue)
            {
                films = films.Where(f => f.FilmCategories.Any(fc => fc.CategoryId == categoryId.Value));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                // Dela upp searchString på mellanslag.
                var searchParts = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // Kontrollerar searchString och jämför med titel eller actor.
                films = films.Where(f =>
                    f.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) || 
                    f.FilmActors.Any(fa =>
                    searchParts.All(part =>
                    fa.Actor.FirstName.Contains(part, StringComparison.OrdinalIgnoreCase) ||
                    fa.Actor.LastName.Contains(part, StringComparison.OrdinalIgnoreCase)
            )
        ) 
    );
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

            //pagination
            int totalFilms = films.Count();
            var totalPages = (int)Math.Ceiling(totalFilms / (double)pageSize);
            var categories = await _filmRepository.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            var filmsPage = films
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;


            return View(filmsPage);
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