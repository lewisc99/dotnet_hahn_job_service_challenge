using Application.Interfaces;
using Hangfire;

namespace JobLibrary
{
    public class BreedJob
    {
        private readonly IBreedService _breedService;

        public BreedJob(IBreedService breedService) => _breedService = breedService;

        [AutomaticRetry(Attempts = 3)]
        public async Task ExecuteAsync() => await _breedService.UpsertBreedsAsync();
    }
}
