using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AutoMapper;
using Kanban.Dto;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = StaticUserRoles.USER)]
public class TeamController : ControllerBase
{
    private readonly IRepository<Team> _teamRepository;
    private readonly IRepository<TeamMember> _teamMemberRepository;
    private readonly IMapper _mapper;

    public TeamController(
        IRepository<Team> teamRepository,
        IRepository<TeamMember> teamMemberRepository,
        IMapper mapper)
    {
        _teamRepository = teamRepository;
        _teamMemberRepository = teamMemberRepository;
        _mapper = mapper;
    }

    [HttpGet("get-teams")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TeamDto>))]
    [Authorize(Roles = StaticUserRoles.USER)]
    public async Task<IActionResult> GetTeams()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //TODO: Rewrite query to exclude TeamMemberRepository
        var teamsQuery = from t in _teamRepository.Query
            join m in _teamMemberRepository.Query on t.Id equals m.TeamId
            where m.UserId == userId
            select t;

        var teams = await teamsQuery.ToListAsync();
        var teamsMap = _mapper.Map<List<TeamDto>>(teams);
        return Ok(teamsMap);
    }

    [HttpPost]
    [ProducesResponseType(200, Type = typeof(TeamDto))]
    [Authorize(Roles = StaticUserRoles.USER)]
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

    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [Authorize(Roles = StaticUserRoles.USER)]
    public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto updateTeamDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var team = await _teamRepository.FindByIdAsync(updateTeamDto.Id);
        if (team == null)
            return NotFound();
        team.Title = updateTeamDto.Title;

        var context = new ValidationContext(team);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(team, context, results, true))
        {
            Console.WriteLine(results);
            foreach (var error in results)
                ModelState.AddModelError("Team", error.ErrorMessage!);
            return BadRequest(ModelState);
        }

        await _teamRepository.SaveAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [Authorize(Roles = StaticUserRoles.USER)]
    public async Task<IActionResult> DeleteTeam(int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var team = await _teamRepository.FindByIdAsync(id);
        if (team == null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (team.OwnerId != userId)
            return Forbid();

        await _teamRepository.DeleteAsync(team);
        return NoContent();
    }
}