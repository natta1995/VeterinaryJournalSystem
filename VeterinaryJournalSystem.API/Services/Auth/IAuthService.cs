using VeterinaryJournalSystem.API.Dtos.Auth;

namespace VeterinaryJournalSystem.API.Services;

public interface IAuthService
{
    Task<object> RegisterAsync(RegisterDto dto);

    Task<object?> LoginAsync(LoginDto dto);
}