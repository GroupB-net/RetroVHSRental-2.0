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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Active)
                .HasConversion(
                    v => v ? "1" : "0",   // bool → string
                    v => v == "1");       // string → bool
        }
    }
}