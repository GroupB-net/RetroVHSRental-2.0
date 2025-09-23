using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("category")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public byte CategoryId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public ICollection<FilmCategory> FilmCategories { get; set; }
    }
}
