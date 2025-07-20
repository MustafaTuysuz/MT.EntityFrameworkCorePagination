using Microsoft.EntityFrameworkCore;

namespace MT.EntityFrameworkCorePagination.Pagination;

public static class PagedListQueryableExtensions
{
    public static async Task<PaginationResult<T>> ToPagedListAsync<T>(
        this IQueryable<T> source,
        int pageNumber,
        int pageSize)
    {
        var count = await source.CountAsync();

        if (count > 0)
        {
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<T>(items, pageNumber, pageSize, count);
        }

        return new PaginationResult<T>(new List<T>(), 0, 0, 0);
    }
}
