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
        //Check request
        if (request == null)
            return BadRequest(ModelState);

        //Check Password and Password Confirm fields
        if (request.Password != request.PasswordConfirm)
        {
            ModelState.AddModelError("Password", "Password and Password Confirm fields don't match");
            return BadRequest(ModelState);
        }

        //Validate Password
        var passwordValidationContext = new ValidationContext(request)
        {
            MemberName = nameof(request.Password)
        };
        var passwordValidationResults = new List<ValidationResult>();
        if (!Validator.TryValidateProperty(
            request.Password,
            passwordValidationContext,
            passwordValidationResults)
        )
        {
            foreach (var error in passwordValidationResults)
                if (error.ErrorMessage != null)
                    ModelState.AddModelError("Password", error.ErrorMessage);
            return BadRequest(ModelState);
        }

        //Validate Model
        var userMap = _mapper.Map<User>(request);
        userMap.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var context = new ValidationContext(userMap);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(userMap, context, results, true))
        {
            foreach (var error in results)
                if (error.ErrorMessage != null)
                    ModelState.AddModelError("errors", error.ErrorMessage);
            return BadRequest(ModelState);
        }

        // Check exists
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

        //Create User
        if (!_userRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("errors", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfuly");
    }
}