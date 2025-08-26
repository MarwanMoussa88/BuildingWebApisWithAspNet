using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingWebApisWithAspNet.Entities
{
    [Table("BoardGameMechanics",Schema ="BoardGame")]
    public class BoardGameMechanic
    {
        [Key]
        [Required]
        public int BoardGameId { get; set; }
        [Key]
        [Required]
        public int MechanicId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public Mechanic Mechanic { get; set; }
        public BoardGame BoardGame { get; set; }

    }
}
