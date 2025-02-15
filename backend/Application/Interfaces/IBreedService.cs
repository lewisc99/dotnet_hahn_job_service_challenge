using Domain.Entities;
using Domain.Entities.Pagination;

namespace Application.Interfaces
{
    public interface IBreedService
    {
        Task UpsertBreedsAsync();

        ApiResponse<Breed> GetPaginatedData(PaginationFilter<Breed> filter);
    }
}
