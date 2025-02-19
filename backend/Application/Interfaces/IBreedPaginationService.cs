using Domain.Entities.Pagination;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBreedPaginationService
    {
        Task<ApiResponse<Breed>> GetPaginatedDataAsync(PaginationFilter<Breed> filter);
    }
}
