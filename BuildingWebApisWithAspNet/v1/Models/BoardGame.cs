namespace BuildingWebApisWithAspNet.V1.Models
{
    public record BoardGame
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public int? MinPlayers { get; set; }
        public int? MaxPlayers { get; set; }
    }
}
