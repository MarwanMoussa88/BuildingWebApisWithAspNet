using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuildingWebApisWithAspNet.Entities
{
    [Table("BoardNameCategories", Schema = "BoardGame")]
    public class BoardGameCategory
    {
        [Key]
        public int BoardGameId { get; set; }
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public BoardGame BoardGame { get; set; }
        public Category Category { get; set; }
    }
}
