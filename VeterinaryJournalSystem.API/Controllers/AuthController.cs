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
    private readonly JwtTokenService _jwtTokenService;


    public AuthController(
        UserManager<StaffUser> userManager,
        SignInManager<StaffUser> signInManager,
       JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
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

        var roles = await _userManager.GetRolesAsync(user);

        var token = _jwtTokenService.CreateToken(user, roles);

        return Ok(new
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
        });
    }
}