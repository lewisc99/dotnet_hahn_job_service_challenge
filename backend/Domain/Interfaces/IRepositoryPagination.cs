using Domain.Entities.Pagination;

namespace Domain.Interfaces
{
    public interface IRepositoryPagination<T>
    {
        Task<ApiResponse<T>> GetPaginatedDataAsync(PaginationFilter<T> filter);
    }
}
