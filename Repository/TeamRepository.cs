using Kanban.Data;
using Kanban.Interfaces;
using Kanban.Models;

namespace Kanban.Repository;

public class TeamRepository : RepositoryBase<Team>
{
    private readonly IRepository<TeamMember> _teamMemberRepository;
    
    public TeamRepository(ApplicationContext context, IRepository<TeamMember> teamMemberRepository) : base(context)
    {
        _teamMemberRepository = teamMemberRepository;
    }

    public override async Task<Team> CreateAsync(Team team)
    {
        // TODO: Refactor to auto create TeamMember
        var newTeam = await Table.AddAsync(team);
        var teamEntity = newTeam.Entity;
        var teamMember = new TeamMember()
        {
            UserId = team.OwnerId,
            Team = teamEntity,
            AccessLevel = AccessLevels.Admin,
            Status = Statuses.Member
        };
        await SaveAsync();
        
        await _teamMemberRepository.CreateAsync(teamMember);
        return teamEntity;
    }
}