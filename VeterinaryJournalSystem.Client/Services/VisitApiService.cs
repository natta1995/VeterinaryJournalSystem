
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace VeterinaryJournalSystem.Client.Services;

public class VisitApiService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _js;

    public VisitApiService(HttpClient httpClient, IJSRuntime js)
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

    public async Task<VisitDto?> CreateVisitAsync(CreateVisitRequest request)
    {
        await AddAuthorizationHeaderAsync();

        var response = await _httpClient.PostAsJsonAsync("api/visits", request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<VisitDto>();
    }
}

public class CreateVisitRequest
{
    public string PetId { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
}

public class VisitDto
{
    public string Id { get; set; } = string.Empty;
    public string PetId { get; set; } = string.Empty;
    public DateTime ScheduledAt { get; set; }
    public string ReasonForVisit { get; set; } = string.Empty;
    public int Status { get; set; }
}