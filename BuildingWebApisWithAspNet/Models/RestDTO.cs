namespace BuildingWebApisWithAspNet.Models
{
    public record RestDTO<T>
    {
        public T Data { get; init; }
        public List<LinkDTO> Links { get; init; } = new();
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
    }
}
