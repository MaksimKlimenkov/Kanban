using Kanban.Data;
using Kanban.Models;

namespace Kanban.Repository;

public class TeamMemberRepository : RepositoryBase<TeamMember>
{
    public TeamMemberRepository(ApplicationContext context) : base(context)
    {
    }
}