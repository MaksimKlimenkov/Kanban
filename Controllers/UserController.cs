using System.Security.Claims;
using AutoMapper;
using Kanban.Dto;
using Kanban.Models;
using Kanban.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    [Authorize(Roles = StaticUserRoles.USER)]
    public async Task<IActionResult> GetUser()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);
        
        if (user == null)
            return NotFound();

        var userMap = _mapper.Map<UserDto>(user);
        return Ok(userMap);
    }

    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
    [Authorize(Roles = StaticUserRoles.ADMIN_OWNER)]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userManager.Users.ToList());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }

    [HttpPut()]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [Authorize(Roles = StaticUserRoles.USER)]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(userId!);

        if (user == null)
            return NotFound();

        if (user.Id != updatedUser.Id)
            return Forbid();

        user.Email = updatedUser.Email;
        user.UserName = updatedUser.UserName;
        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        
        var userValidator = new UserValidator<User>();
        var validateResult = await userValidator.ValidateAsync(_userManager, user);

        if (!validateResult.Succeeded)
        {
            foreach (var error in validateResult.Errors)
                ModelState.AddModelError("User", error.Description);
            return BadRequest(ModelState);
        }

        var updateResult = await _userManager.UpdateAsync(user);

        if (updateResult.Succeeded) return NoContent();
        
        foreach (var error in updateResult.Errors)
            ModelState.AddModelError("errors", error.Description);
        return StatusCode(500, ModelState);
    }
}