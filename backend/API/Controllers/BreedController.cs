using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IBreedPaginationService _breedService;
        public WeatherForecastController(IBreedPaginationService breedService) => _breedService = breedService;

        [HttpPost]
        public async Task<ApiResponse<Breed>> get(PaginationFilter<Breed> filter) => await _breedService.GetPaginatedDataAsync(filter);
    }
}
