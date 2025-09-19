using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RetroVHSRental.Models;
using RetroVHSRental.Repository;

namespace RetroVHSRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFilmRepository _filmRepository;
        private readonly IRentalRepository _rentalRepository;


        public HomeController(ILogger<HomeController> logger, IFilmRepository filmRepository, IRentalRepository rentalRepository)
        {
            _logger = logger;
            _filmRepository = filmRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<IActionResult> Index()
        {
            //var films = await _filmRepository.GetAllAsync();
            //return View(films);

            var rentals = await _rentalRepository.RentalsExpiringToday();
            ViewBag.ExpiringRentals = rentals;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
