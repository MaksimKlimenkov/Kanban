using Kanban.Data;
using Kanban.Models;

namespace Kanban.Repository;

public class TeamRepository : RepositoryBase<Team>
{
    public TeamRepository(ApplicationContext context) : base(context)
    {
    }

    public override async Task<Team> CreateAsync(Team team)
    {
        var teamMember = new TeamMember
        {
            UserId = team.OwnerId,
            AccessLevel = AccessLevels.Admin,
            Status = Statuses.Member
        };
        team.Members.Add(teamMember);
        var teamEntry = await Table.AddAsync(team);

        await SaveAsync();
        return teamEntry.Entity;
    }
}