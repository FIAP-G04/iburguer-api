namespace FIAP.Diner.Application.Common;

public record PaginatedList<T> where T : class
{
    public int Total { get; set; }
    public int? Page { get; set; }
    public int? Limit { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}