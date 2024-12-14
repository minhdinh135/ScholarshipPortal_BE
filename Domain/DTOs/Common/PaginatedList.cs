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
    
    // public static PaginatedList<T> ToPaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
    // {
    //     var count = source.Count();
    //     var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    //     return new PaginatedList<T>(items, pageIndex, count);
    // }    
}
