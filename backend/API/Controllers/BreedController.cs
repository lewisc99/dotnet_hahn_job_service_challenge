using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BreedController : ControllerBase
    {
        private IBreedPaginationService _breedService;
        public BreedController(IBreedPaginationService breedService) => _breedService = breedService;

        [HttpGet("[Action]")]
        public async Task<ApiResponse<Breed>> get([FromQuery] PaginationFilter<Breed> filter) => await _breedService.GetPaginatedDataAsync(filter);
    }
}
