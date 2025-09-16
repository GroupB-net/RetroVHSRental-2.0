using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.CodeAnalysis.Elfie.Extensions;

namespace RetroVHSRental.Models
{
    [Table("staff")]
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("staff_id")]
        public byte StaffId { get; set; }
        [Column("first_name")]
        public required string FirstName { get; set; }
        [Column("last_name")]
        public required string LastName { get; set; }
        [Column("address_id")]
        public int AddressId { get; set; }
        [Column("email")]
        public required string Email { get; set; }
        [Column("store_id")]
        public int StoreId { get; set; }
        [Column("username")]
        public required string Username { get; set; }
        [Column("password")]
        public required string Password { get; set; }
        [Column("last_update")]
        public DateTime Last_Update { get; set; }
    }
}
