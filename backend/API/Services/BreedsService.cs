using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;

namespace Services;

public class BreedsService: IBreedService
{
    private readonly IRepositoryPagination<Breed> _repository;

    public BreedsService(IRepositoryPagination<Breed> repository) => _repository = repository;

    public ApiResponse<Breed> GetPaginatedData(PaginationFilter<Breed> filter) => _repository.GetPaginatedData(filter);
}
