using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("film")]
    public class Film
    {
        [Key]
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("release_year")]
        public string Release_year { get; set; }
        [Column("language_id")]
        public byte LanguageId { get; set; }
        [Column("rental_duration")]
        public byte Rental_duration {  get; set; }
        [Column("rental_rate")]
        public decimal Rental_rate { get; set; }

        public Language Language { get; set; }
        public ICollection<FilmCategory> FilmCategories { get; set; }
        public ICollection<FilmActor> FilmActors { get; set; }
    }
}
