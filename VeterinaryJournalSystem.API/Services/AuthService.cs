using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.Application.Dtos.Auth;
using VeterinaryJournalSystem.Domain.Entities;
using VeterinaryJournalSystem.Application.Services.Auth;

namespace VeterinaryJournalSystem.API.Services.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<StaffUser> _userManager;
    private readonly SignInManager<StaffUser> _signInManager;
    private readonly JwtTokenService _jwtTokenService;

    public AuthService(
        UserManager<StaffUser> userManager,
        SignInManager<StaffUser> signInManager,
        JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<object> RegisterAsync(RegisterDto dto)
    {
        var existingStaffCode = await _userManager.Users
            .FirstOrDefaultAsync(u => u.StaffCode == dto.StaffCode);

        if (existingStaffCode != null)
            throw new Exception("Staff code already exists.");

        var user = new StaffUser
        {
            FullName = dto.FullName,
            StaffCode = dto.StaffCode,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

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
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.StaffCode == dto.StaffCode);

        if (user == null)
            return null;

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

        if (!result.Succeeded)
            return null;

        var roles = await _userManager.GetRolesAsync(user);
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