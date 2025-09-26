using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private readonly IInventoryRepository inventoryRepository;
        public RentalController(IRentalRepository rentalRepository, ICustomerRepository customerRepository, IFilmRepository filmRepository, IStaffRepository staffRepository, IInventoryRepository inventoryRepository)
        {
            _rentalRepository = rentalRepository;
            _customerRepository = customerRepository;
            _filmRepository = filmRepository;
            this.inventoryRepository = inventoryRepository;
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
            var customers = await _customerRepository.GetAllAsync();
            var film = await _filmRepository.GetByIdAsync(id);
            var inventories = await inventoryRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            //Sorting out all the posts in dbo.rental where ReturnDate=null so that you can't rent the same movie at the same time. 
            var rentedInventoryIds = await _rentalRepository.GetAllAsync();
            var rentedIds = rentedInventoryIds
                .Where(r => r.ReturnDate == null)
                .Select(r => r.InventoryId)
                .ToList();
            var availableInventories = inventories
                .Where(i => !rentedIds.Contains(i.InventoryId))
                .ToList();

            ViewBag.Customer = new SelectList(customers.OrderBy(c => c.Email), "CustomerId", "Email");
            ViewBag.Inventory = new SelectList(availableInventories.Where(f => f.FilmId == id).Where(s => s.StoreId == 1), "InventoryId", "InventoryId");
            ViewBag.Film = film;
            ViewBag.Staff = new SelectList(staff.OrderBy(s => s.FirstName).Select(s => new { StaffId = s.StaffId, FullName = s.FirstName + " Store: " + s.StoreId }), "StaffId", "FullName");

            var rental = new Rental { RentalDate = DateTime.Now, FilmId=id};
            return View(rental);
        }

        // POST: RentalController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            rental.last_update = DateTime.Now;
            if (ModelState.IsValid)
            {
                await _rentalRepository.AddAsync(rental);
                return RedirectToAction(nameof(Index));
            }

            // Samma logik som i GET för att bara visa ledig inventory om modelstate misslyckas
            var customers = await _customerRepository.GetAllAsync();
            var inventories = await inventoryRepository.GetAllAsync();
            var staff = await _staffRepository.GetAllAsync();

            var rentedInventoryIds = await _rentalRepository.GetAllAsync();
            var rentedIds = rentedInventoryIds
                .Where(r => r.ReturnDate == null)
                .Select(r => r.InventoryId)
                .ToList();

            var availableInventories = inventories
                .Where(i => !rentedIds.Contains(i.InventoryId))
                .ToList();

            ViewBag.Customer = new SelectList(customers.OrderBy(c => c.Email), "CustomerId", "Email");
            ViewBag.Inventory = new SelectList(
                availableInventories.Where(f => f.FilmId == rental.FilmId).Where(s => s.StoreId == 1), "InventoryId", "InventoryId");
            ViewBag.Film = await _filmRepository.GetByIdAsync(rental.FilmId);
            ViewBag.Staff = new SelectList(staff.OrderBy(s => s.StaffId), "StaffId", "FirstName");

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
        [HttpPost, ActionName("EditReturnDate")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmed(int rentalId)
        {
            try
            {
                var rental = await _rentalRepository.GetByIdAsync(rentalId);
                rental.ReturnDate = DateTime.Now;
                if (rental != null)
                {
                    await _rentalRepository.UpdateAsync(rental);

                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var rental = await _rentalRepository.GetByIdAsync(id);
                if (rental != null)
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
            var expiringRentals = await _rentalRepository.RentalsExpiringToday();
            ViewBag.ExpiringRentals = expiringRentals;
            return View();

        }
    }
}
