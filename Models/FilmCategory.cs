using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("film_category")]
    public class FilmCategory
    {
        [Column("film_id")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Column("category_id")]
        public byte CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
