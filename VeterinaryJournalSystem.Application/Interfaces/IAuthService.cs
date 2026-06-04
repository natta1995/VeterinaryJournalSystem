using VeterinaryJournalSystem.Application.Dtos.Auth;

namespace VeterinaryJournalSystem.Application.Interfaces;

public interface IAuthService
{
    Task<object> RegisterAsync(RegisterDto dto);

    Task<object?> LoginAsync(LoginDto dto);
}