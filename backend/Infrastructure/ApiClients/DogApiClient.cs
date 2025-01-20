using Domain.Entities;
using Domain.Entities.Pagination;
using Newtonsoft.Json;

namespace Infrastructure.ApiClients
{
    public class DogApiClient
    {
        private readonly HttpClient _httpClient;

        public DogApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("DogApi");
        }

        public async Task<ApiResponse<Breed>> FetchBreedsAsync(string? relativeUrl = "breeds")
        {
            var response = await _httpClient.GetAsync(relativeUrl);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<Breed>>(content);
        }
    }
}
