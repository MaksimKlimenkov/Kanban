using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(200, Type = typeof(UserDto))]
    [ProducesResponseType(404)]
    public IActionResult GetUser(int id)
    {
        var user = _mapper.Map<UserDto>(
            _userRepository.GetUser(id)
        );
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet()]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }

    [HttpPut("{userId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public IActionResult UpdateUser(int userId, [FromBody] UserDto updatedUser)
    {
        if (updatedUser == null)
            return BadRequest(ModelState);

        if (userId != updatedUser.Id)
            return BadRequest(ModelState);

        if (!_userRepository.UserExists(userId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest();

        var userMap = _mapper.Map<User>(updatedUser);
        var context = new ValidationContext(userMap);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(userMap, context, results, true))
        {
            foreach (var error in results)
                if (error.ErrorMessage != null)
                    ModelState.AddModelError("errors", error.ErrorMessage);
            return BadRequest(ModelState);
        }

        if (!_userRepository.UpdateUser(userMap))
        {
            ModelState.AddModelError("errors", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}