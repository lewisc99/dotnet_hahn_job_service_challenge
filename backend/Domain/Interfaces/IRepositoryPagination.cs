using Domain.Entities.Pagination;

namespace Domain.Interfaces
{
    public interface IRepositoryPagination<T>
    {
        ApiResponse<T> GetPaginatedData(PaginationFilter<T> filter);
    }
}
