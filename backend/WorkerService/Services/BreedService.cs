using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.ApiClients;

namespace WorkerService
{
    public class BreedService : IBreedService
    {
        private readonly IRepository<Breed> _repository;
        private readonly DogApiClient _apiClient;

        public BreedService(IRepository<Breed> repository, DogApiClient apiClient)
        {
            _repository = repository;
            _apiClient = apiClient;
        }

        public async Task UpsertBreedsAsync()
        {
            string? nextPageLink = "breeds";
            int addedCount = 0;

            while (!string.IsNullOrEmpty(nextPageLink) && addedCount < 5)
            {
                var response = await _apiClient.FetchBreedsAsync(nextPageLink);

                if (response?.Data == null)
                {
                    Console.WriteLine($"Error fetching breeds: {response?.Message ?? "Unknown error"}");
                    break;
                }

                foreach (var breedData in response.Data)
                {
                    var id = breedData.Id;

                    if (await _repository.ExistsAsync(id))
                        continue;

                    var breed = new Breed
                    {
                        Id = id,
                        Name = breedData.Name,
                        Description = string.IsNullOrEmpty(breedData.Description) ? "" : breedData.Description,
                        LifeMin = breedData.LifeMin,
                        LifeMax = breedData.LifeMax,
                        MaleWeightMin = breedData.MaleWeightMin,
                        MaleWeightMax = breedData.MaleWeightMax,
                        FemaleWeightMin = breedData.FemaleWeightMin,
                        FemaleWeightMax = breedData.FemaleWeightMax,
                        Hypoallergenic = breedData.Hypoallergenic
                    };

                    await _repository.AddAsync(breed);
                    addedCount++;

                    if (addedCount >= 5)
                        break;
                }

                nextPageLink = response.NextPageLink;
            }

            await _repository.SaveChangesAsync();
        }
    }
}
