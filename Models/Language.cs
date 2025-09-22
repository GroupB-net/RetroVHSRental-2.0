using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("language")]
    public class Language
    {
        [Key]
        [Column("language_id")]
        public byte LanguageId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public ICollection<Film> Films { get; set; }
    }
}
