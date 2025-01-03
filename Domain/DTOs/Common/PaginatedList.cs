namespace Domain.DTOs.Common;

public class PaginatedList<T>
{
    public IEnumerable<T> Items { get; set; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public int TotalCount { get; set; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
    }
}