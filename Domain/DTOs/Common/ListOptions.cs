namespace Domain.DTOs.Common;

public class ListOptions
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? SortBy { get; set; } = "Id";

    public bool IsDescending { get; set; } = false;

    public bool IsPaging { get; set; } = false;
}