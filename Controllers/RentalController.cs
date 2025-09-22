using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetroVHSRental.Models;
using RetroVHSRental.Repository;

namespace RetroVHSRental.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IStaffRepository _staffRepository;
        public RentalController(IRentalRepository rentalRepository, ICustomerRepository customerRepository, IFilmRepository filmRepository, IStaffRepository staffRepository)
        {
            _rentalRepository = rentalRepository;
            _customerRepository = customerRepository;
            _filmRepository = filmRepository;
            _staffRepository = staffRepository;
        }
        // GET: RentalController
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20;

            int totalItems = await _rentalRepository.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var rentals = await _rentalRepository.GetPagedAsync(page, pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(rentals);
        }

        // GET: RentalController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // GET: RentalController/Create
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Customer = await _customerRepository.GetAllAsync();
            ViewBag.Film = await _filmRepository.GetByIdAsync(id);
            ViewBag.Staff = await _staffRepository.GetAllAsync();

            return View();
        }

        // POST: RentalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
              {
                  rental.RentalDate = DateTime.Now;
                  await _rentalRepository.AddAsync(rental);
                  return RedirectToAction(nameof(Index));
              }

            ViewBag.Customer = await _customerRepository.GetAllAsync();
            ViewBag.Film = await _filmRepository.GetAllAsync();
            ViewBag.Staff = await _staffRepository.GetAllAsync();
            return View(rental);
         
        }

        // GET: RentalController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: RentalController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Rental rental)
        {
            if (ModelState.IsValid)
            {
                await _rentalRepository.UpdateAsync(rental);
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }

        // GET: RentalController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return View(rental);
        }

        // POST: RentalController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var rental = await _rentalRepository.GetByIdAsync(id);
                if (rental!=null)
                {
                    await _rentalRepository.RemoveAsync(rental);

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ExpiringToday()
        {
            ViewBag.Rental = await _rentalRepository.RentalsExpiringToday();
            return View();

        }
    }
}
