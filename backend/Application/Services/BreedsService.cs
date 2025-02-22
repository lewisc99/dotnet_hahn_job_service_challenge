using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Pagination;
using Domain.Interfaces;

namespace Services;

public class BreedsService: IBreedPaginationService
{
    private readonly IRepositoryPagination<Breed> _repository;

    public BreedsService(IRepositoryPagination<Breed> repository) => _repository = repository;

    public async Task<ApiResponse<Breed>> GetPaginatedDataAsync(PaginationFilter<Breed> filter) => await _repository.GetPaginatedDataAsync(filter);
}
