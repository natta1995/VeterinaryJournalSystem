using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace VeterinaryJournalSystem.Client.Services;

public class OwnerApiService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;

    public OwnerApiService(HttpClient httpClient, IJSRuntime js)
    {
        _httpClient = httpClient;
        _js = js;
    }

    private async Task AddAuthorizationHeaderAsync()
    {
        var token = await _js.InvokeAsync<string?>(
            "localStorage.getItem",
            "authToken");

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<OwnerDto?> SearchOwnerByPersonalNumberAsync(string personalNumber)
    {
        await AddAuthorizationHeaderAsync();

        var response = await _httpClient.GetAsync(
            $"api/owners/search?personalNumber={personalNumber}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<OwnerDto>();
    }

    public async Task<OwnerDto?> CreateOwnerAsync(CreateOwnerRequest request)
    {
        await AddAuthorizationHeaderAsync();

        var response = await _httpClient.PostAsJsonAsync("api/owners", request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<OwnerDto>();
    }

  
}



public class CreateOwnerRequest
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string? Comment { get; set; }
}
public class OwnerDto
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PersonalNumber { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public List<PetDto> Pets { get; set; } = new();
}

public class PetDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
}
