using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


}