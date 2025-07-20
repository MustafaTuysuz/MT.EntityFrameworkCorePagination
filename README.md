# MT.EntityFrameworkCorePagination

**MT.EntityFrameworkCorePagination** is a lightweight and async pagination utility for Entity Framework Core.  
It simplifies data pagination by adding a `ToPagedListAsync()` extension method to `IQueryable<T>`, returning a structured result that includes the current page, total pages, page size, item list, and first/last page indicators.

---

## 📦 Installation

Install the NuGet package:

```bash
dotnet add package MT.EntityFrameworkCorePagination --version 1.0.0
```

## Usage
```CSsharp
using MT.EntityFrameworkCorePagination.Pagination;

var pagedResult = await dbContext.Users
    .Where(u => u.IsActive)
    .ToPagedListAsync(pageNumber: 1, pageSize: 10);

// Access the result
var users = pagedResult.Items;
var totalPages = pagedResult.TotalPages;
var isFirst = pagedResult.IsFirstPage;
var isLast = pagedResult.IsLastPage;
```

## Models
```
using System;
using System.Collections.Generic;

namespace MT.EntityFrameworkCorePagination.Pagination
{
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
}
```

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MT.EntityFrameworkCorePagination.Pagination
{
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
}

```
