using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
}