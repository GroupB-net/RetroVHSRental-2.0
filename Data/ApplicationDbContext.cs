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
        public DbSet<Staff> Staff {  get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<FilmCategory> FilmCategories { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }
        public DbSet<Category> Categories { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Active)
                .HasConversion(
                    v => v ? "1" : "0",   // bool → string
                    v => v == "1");       // string → bool

            // Many->Many: Film <-> Category
            modelBuilder.Entity<FilmCategory>()
                .HasKey(fc => new { fc.FilmId, fc.CategoryId });

            // Many->Many: Film <-> Actor
            modelBuilder.Entity<FilmActor>()
                .HasKey(fa => new { fa.FilmId, fa.ActorId });

            // Film -> Language (One->Many)
            modelBuilder.Entity<Film>()
                .HasOne(f => f.Language)
                .WithMany(l => l.Films)
                .HasForeignKey(f => f.LanguageId);

        }
    }
}