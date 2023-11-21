using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Kanban.Dto.Auth;
using Kanban.Interfaces;
using Kanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost("register")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult Register([FromBody] RegisterDto request)
    {
        if (request == null)
            return BadRequest(ModelState);

        if (request.Password != request.PasswordConfirm)
        {
            ModelState.AddModelError("Password", "Password and Confirm fields don't match");
            return BadRequest(ModelState);
        }

        var user = _userRepository.GetUsers()
            .Where(u =>
                u.Username.Trim().ToLower() == request.Username.Trim().ToLower() ||
                u.Email.Equals(request.Email)
            )
            .FirstOrDefault();

        if (user != null)
        {
            ModelState.AddModelError("errors", "User already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userMap = _mapper.Map<User>(request);

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        userMap.PasswordHash = passwordHash;

        var context = new ValidationContext(userMap);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(userMap, context, results, true))
        {
            foreach (var error in results)
                if (error.ErrorMessage != null)
                    ModelState.AddModelError("errors", error.ErrorMessage);
            return BadRequest(ModelState);
        }

        if (!_userRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("errors", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfuly");
    }
}