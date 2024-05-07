using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Repository.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(1)]
public class TeamMemberController : ControllerBase
{
    private readonly IRepository<TeamMember> _teamMemberRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public TeamMemberController(IRepository<TeamMember> teamMemberRepository, UserManager<User> userManager,
        IMapper mapper)
    {
        _teamMemberRepository = teamMemberRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    [HttpGet("members/{id:int}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<UserDto>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetTeamMembers(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var member = await _teamMemberRepository.Query.FindByUser(userId!).FindByTeam(id).FirstOrDefaultAsync();
        if (member == null)
            return Forbid();

        var membersQuery = from m in _teamMemberRepository.Query
            where m.TeamId == id
            join u in _userManager.Users on m.UserId equals u.Id
            select u;

        var members = await membersQuery.ToListAsync();
        var usersMap = _mapper.Map<List<UserDto>>(members);

        return Ok(usersMap);
    }

    [HttpGet("member-info")]
    [ProducesResponseType(200, Type = typeof(TeamMemberDto))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> MemberInfo([FromQuery] string userId, [FromQuery] int teamId)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var currentMember = await _teamMemberRepository.Query.FindByUser(currentUserId!).FindByTeam(teamId)
            .FirstOrDefaultAsync();
        if (currentMember == null)
            return Forbid();

        var member = await _teamMemberRepository.Query.FindByUser(userId).FindByTeam(teamId).FirstOrDefaultAsync();
        if (member == null)
            return NotFound();

        var memberMap = _mapper.Map<TeamMemberDto>(member);
        return Ok(memberMap);
    }
}