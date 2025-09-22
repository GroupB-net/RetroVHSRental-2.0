using System.ComponentModel.DataAnnotations.Schema;

namespace RetroVHSRental.Models
{
    [Table("film_actor")]
    public class FilmActor
    {
        [Column("actor_id")]
        public int FilmId { get; set; }
        public Film Film { get; set; }
        [Column("film_id")]
        public int ActorId { get; set; }
        public Actor Actor{ get; set; }
    }
}
