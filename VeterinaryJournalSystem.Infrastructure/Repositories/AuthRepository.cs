using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.Application.Interfaces;
using VeterinaryJournalSystem.Domain.Entities;

namespace VeterinaryJournalSystem.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly UserManager<StaffUser> _userManager;
    private readonly SignInManager<StaffUser> _signInManager;

    public AuthRepository(
        UserManager<StaffUser> userManager,
        SignInManager<StaffUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> StaffCodeExistsAsync(string staffCode)
    {
        return await _userManager.Users
            .AnyAsync(u => u.StaffCode == staffCode);
    }

    public async Task<StaffUser?> GetUserByStaffCodeAsync(string staffCode)
    {
        return await _userManager.Users
            .FirstOrDefaultAsync(u => u.StaffCode == staffCode);
    }

    public async Task<(bool Succeeded, IEnumerable<string> Errors)> CreateUserAsync(
        StaffUser user,
        string password)
    {
        var result = await _userManager.CreateAsync(user, password);

        return (
            result.Succeeded,
            result.Errors.Select(e => e.Description)
        );
    }

    public async Task<bool> CheckPasswordAsync(StaffUser user, string password)
    {
        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            password,
            false);

        return result.Succeeded;
    }

    public async Task<IList<string>> GetRolesAsync(StaffUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}