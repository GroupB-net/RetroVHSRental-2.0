using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("customer")]
    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        public bool Active { get; set; }
        [Column("create_date")]
        public DateTime CreateDate { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
