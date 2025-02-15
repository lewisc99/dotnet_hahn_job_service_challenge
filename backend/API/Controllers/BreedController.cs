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
        private IBreedService _breedService;
        public WeatherForecastController(IBreedService breedService) => _breedService = breedService;

        [HttpPost]
        public ApiResponse<Breed> get(PaginationFilter<Breed> filter) => _breedService.GetPaginatedData(filter);
    }
}
