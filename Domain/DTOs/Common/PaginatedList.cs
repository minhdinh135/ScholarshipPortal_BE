namespace Domain.DTOs.Common;

public class PaginatedList<T>
{
    public IEnumerable<T> Items { get; set; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedList(IEnumerable<T> items, int pageIndex, int totalPages)
    {
        Items = items;
        PageIndex = pageIndex;
        TotalPages = totalPages;
    } 
}
