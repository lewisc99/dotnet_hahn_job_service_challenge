using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;

namespace Services;

public class BreedsService
{
    private readonly IRepositoryPagination<Breed> _repository;

    public BreedsService(IRepositoryPagination<Breed> repository)
    {
        _repository = repository;
    }

    public ApiResponse<Breed> GetPaginatedData(PaginationFilter<Breed> filter)
    {
        return _repository.GetPaginatedData(filter);
    }
}
