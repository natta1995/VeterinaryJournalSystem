using VeterinaryJournalSystem.Application.Dtos.Auth;
using VeterinaryJournalSystem.Application.Interfaces;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenService _jwtTokenService;

    public AuthService(
        IAuthRepository authRepository,
        IJwtTokenService jwtTokenService)
    {
        _authRepository = authRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<object> RegisterAsync(RegisterDto dto)
    {
        var staffCodeExists = await _authRepository.StaffCodeExistsAsync(dto.StaffCode);

        if (staffCodeExists)
        {
            throw new Exception("Staff code already exists.");
        }

        var user = new StaffUser
        {
            FullName = dto.FullName,
            StaffCode = dto.StaffCode,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _authRepository.CreateUserAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors));
        }

        return new
        {
            user.Id,
            user.FullName,
            user.StaffCode,
            user.Email
        };
    }

    public async Task<object?> LoginAsync(LoginDto dto)
    {
        var user = await _authRepository.GetUserByStaffCodeAsync(dto.StaffCode);

        if (user == null)
        {
            return null;
        }

        var passwordIsValid = await _authRepository.CheckPasswordAsync(user, dto.Password);

        if (!passwordIsValid)
        {
            return null;
        }

        var roles = await _authRepository.GetRolesAsync(user);
        var token = _jwtTokenService.CreateToken(user, roles);

        return new
        {
            token,
            user = new
            {
                user.Id,
                user.FullName,
                user.StaffCode,
                user.Email,
                roles
            }
        };
    }
}