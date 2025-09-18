using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("inventory")]
    public class Inventory
    {
        [Key]
        [Column("inventory_id")]
        public int InventoryId { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
