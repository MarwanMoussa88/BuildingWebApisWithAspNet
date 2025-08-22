namespace BuildingWebApisWithAspNet.Models
{
    public record RestDTO<T>
    {
        public T Items { get; init; }
        public List<LinkDTO> Links { get; init; } = new();
    }
}
