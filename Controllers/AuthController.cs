using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("seed-roles")]
    public async Task<IActionResult> SeedRoles()
    {
        bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
        bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
        bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

        if (!isOwnerRoleExists && !isAdminRoleExists && !isUserRoleExists) return Ok("Seeded");

        await _roleManager.CreateAsync(new(StaticUserRoles.OWNER));
        await _roleManager.CreateAsync(new(StaticUserRoles.ADMIN));
        await _roleManager.CreateAsync(new(StaticUserRoles.USER));

        return Ok("Seeded");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        // Validate
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var isExistsUser = await _userManager.FindByNameAsync(registerDto.UserName);

        if (isExistsUser != null)
        {
            ModelState.AddModelError("errors", $"UserName '{registerDto.UserName}' is already taken.");
            return BadRequest(ModelState);
        }

        // Create User
        User newUser = new()
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(newUser, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("errors", error.Description);
            return BadRequest(ModelState);
        }

        // Add default user role to new user
        await _userManager.AddToRoleAsync(newUser, StaticUserRoles.USER);

        return Ok("Successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        // Validate
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(loginDto.Query);
        user ??= await _userManager.FindByEmailAsync(loginDto.Query);

        if (user == null)
        {
            ModelState.AddModelError("errors", "Invalid Credentails");
            return Unauthorized(ModelState);
        }

        bool isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordCorrect)
        {
            ModelState.AddModelError("errors", "Invalid Credentails");
            return Unauthorized(ModelState);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim("JWTID", Guid.NewGuid().ToString()),
        };

        foreach (var role in userRoles)
            authClaims.Add(new Claim(ClaimTypes.Role, role));

        var token = GenerateNewJwt(authClaims);


        return Ok(token);
    }

    private string GenerateNewJwt(List<Claim> authClaims)
    {
        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            expires: DateTime.Now.AddDays(7),
            claims: authClaims,
            signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

// ValidIssuer = builder.Configuration["JwtSettings:ValidIssuer"],
// ValidAudience = builder.Configuration["JwtSettings:ValidAudience"],
// IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))