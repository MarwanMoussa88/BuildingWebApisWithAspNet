using System.ComponentModel.DataAnnotations;

namespace BuildingWebApisWithAspNet.Models
{
    public record UpdateBoardGameDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
    }
}
