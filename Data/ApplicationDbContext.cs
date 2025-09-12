using Microsoft.EntityFrameworkCore;
using RetroVHSRental.Models;

namespace RetroVHSRental.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Customer> Customers { get; set; }


    }
}