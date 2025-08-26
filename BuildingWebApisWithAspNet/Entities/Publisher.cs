using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingWebApisWithAspNet.Entities
{
    [Table("Publishers", Schema = "BoardGame")]
    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public int Name { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime LastModifiedDate { get; set; }

        public ICollection<BoardGame> BoardGames { get; set; }

    }
}
