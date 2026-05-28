using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryJournalSystem.API.Dtos;
using VeterinaryJournalSystem.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<StaffUser> _userManager;
    private readonly SignInManager<StaffUser> _signInManager;

    public AuthController(
        UserManager<StaffUser> userManager,
        SignInManager<StaffUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var existingStaffCode = await _userManager.Users
            .FirstOrDefaultAsync(u => u.StaffCode == dto.StaffCode);

        if (existingStaffCode != null)
        {
            return BadRequest("Staff code already exists.");
        }

        var user = new StaffUser
        {
            FullName = dto.FullName,
            StaffCode = dto.StaffCode,
            Email = dto.Email,
            UserName = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok("User registered successfully.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.StaffCode == dto.StaffCode);

        if (user == null)
        {
            return Unauthorized("Invalid staff code or password.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            dto.Password,
            false
        );

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid staff code or password.");
        }

        return Ok("Login successful.");
    }
}