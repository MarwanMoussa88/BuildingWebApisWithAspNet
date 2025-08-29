using System.ComponentModel.DataAnnotations;

namespace BuildingWebApisWithAspNet.Models
{
    public record UpdateMechanicDTO
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
