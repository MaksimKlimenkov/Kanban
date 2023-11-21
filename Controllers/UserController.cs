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
    [ProducesResponseType(200, Type = typeof(User))]
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
    [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
    public IActionResult GetUsers()
    {
        var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(users);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateUser([FromBody] UserDto userCreate)
    {
        if (userCreate == null)
            return BadRequest(ModelState);

        var user = _userRepository.GetUsers()
            .Where(u =>
                u.Username.Trim().ToLower() == userCreate.Username.Trim().ToLower() ||
                u.Email.Trim().ToLower() == userCreate.Email.Trim().ToLower()
            )
            .FirstOrDefault();

        if (user != null)
        {
            ModelState.AddModelError("", "User already exists");
            return StatusCode(422, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userMap = _mapper.Map<User>(userCreate);
        if (!_userRepository.CreateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong while saving");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfuly");
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

        if (!_userRepository.UpdateUser(userMap))
        {
            ModelState.AddModelError("", "Something went wrong updating user");
            return StatusCode(500, ModelState);
        }

        return NoContent();
    }
}