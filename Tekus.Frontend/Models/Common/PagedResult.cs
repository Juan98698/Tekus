namespace Tekus.Frontend.Models.Common
{
    public class PagedResult<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public IReadOnlyCollection<T> Items { get; set; } = Array.Empty<T>();
    }
}
