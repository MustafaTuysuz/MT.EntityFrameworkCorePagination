namespace MT.EntityFrameworkCorePagination.Pagination;

public class PaginationResult<T>
{
    public PaginationResult(IList<T> items, int pageNumber, int pageSize, int totalCount)
    {
        Items = items;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        IsFirstPage = pageNumber == 1;
        IsLastPage = pageNumber == TotalPages;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IList<T> Items { get; set; }
    public bool IsFirstPage { get; set; }
    public bool IsLastPage { get; set; }
}
