using System.Net.Http.Json;

namespace VeterinaryJournalSystem.Client.Services;

public class AuthApiService
{
    private readonly HttpClient _httpClient;

    public AuthApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(string staffCode, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/login", new
        {
            staffCode,
            password
        });

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}