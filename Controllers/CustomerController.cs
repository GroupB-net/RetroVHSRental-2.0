using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroVHSRental.Data;
using RetroVHSRental.Models;
using RetroVHSRental.Repository;

namespace RetroVHSRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: Customer
        public async Task<IActionResult> Index(string searchString)
        {
            var customers = await customerRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(f => f.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return View(customers);
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            customer.AddressId = 1;
            customer.Active = true;
            customer.CreateDate = DateTime.Now;
            customer.LastUpdate = DateTime.Now;
            if (ModelState.IsValid)
            {
                await customerRepository.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    customer.LastUpdate = DateTime.Now;
                    await customerRepository.UpdateAsync(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var customer = await customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await customerRepository.GetByIdAsync(id);
            if (customer != null)
            {
                await customerRepository.RemoveAsync(customer);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            var customer = customerRepository.GetByIdAsync(id);
            if(customer == null)
            {
                return false;
            }
            return true;
        }
    }
}
