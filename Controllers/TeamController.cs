using System.Security.Claims;
using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = StaticUserRoles.USER)]
public class TeamController : ControllerBase
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IMapper _mapper;
    
    public TeamController(IRepository<Team> teamRepository, IMapper mapper)
    {
        _teamRepository = teamRepository;
        _mapper = mapper;
    }
    
    [HttpPost()]
    [ProducesResponseType(200, Type = typeof(TeamDto))]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto createTeamDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var teamMap = _mapper.Map<Team>(createTeamDto);
        teamMap.OwnerId = userId!;
        var team = await _teamRepository.CreateAsync(teamMap);
        var teamDto = _mapper.Map<TeamDto>(team);
        
        return Ok(teamDto);
    }
}