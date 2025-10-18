using Library.Models;

namespace Library.Services
{
    public static class PaginationService
    {
        public static PagedResult<T> GetPagedResult<T>(IQueryable<T> query, int pageNumber, int pageSize)
        {
            var totalCount = query.Count();
            var items = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

