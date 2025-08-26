using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingWebApisWithAspNet.Entities
{

    [Table("Mechanics", Schema = "BoardGame")]
    public class Mechanic
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; }
        [Required]
        public int Flags { get; set; }

        public ICollection<BoardGameMechanic> BoardGameMechanics { get; set; }

    }
}
