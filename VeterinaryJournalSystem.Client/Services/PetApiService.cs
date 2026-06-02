using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace VeterinaryJournalSystem.Client.Services;

public class PetApiService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;

    public PetApiService(HttpClient httpClient, IJSRuntime js)
    {
        _httpClient = httpClient;
        _js = js;
    }

    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await _js.InvokeAsync<string?>("localStorage.getItem", "authToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<PetDto>> GetPetsByOwnerIdAsync(string ownerId)
    {
        await AddAuthorizationHeaderAsync();

        var pets = await _httpClient.GetFromJsonAsync<List<PetDto>>(
            $"api/pets/by-owner/{ownerId}");

        return pets ?? new List<PetDto>();
    }

    public async Task<PetDto?> CreatePetAsync(CreatePetRequest request)
    {
        await AddAuthorizationHeaderAsync();

        var response = await _httpClient.PostAsJsonAsync("api/pets", request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PetDto>();
    }
}

public class CreatePetRequest
{
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public bool IsInsured { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string OwnerId { get; set; } = string.Empty;
}
