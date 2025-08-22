namespace BuildingWebApisWithAspNet.V1.Models
{
    public record RestDTO<T>
    {
        public T Data { get; init; }
        public List<LinkDTO> Links { get; init; } = new();
    }
}
