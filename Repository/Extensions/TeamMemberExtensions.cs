using Kanban.Models;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repository.Extensions;

public static class TeamMemberExtensions
{
    public static IQueryable<TeamMember> FindByUser(this IQueryable<TeamMember> query, string userId)
    {
        return query.Where(m => m.UserId == userId);
    }
    
    public static IQueryable<TeamMember> FindByTeam(this IQueryable<TeamMember> query, int teamId)
    {
        return query.Where(m => m.TeamId == teamId);
    }
    
    public static IQueryable<TeamMember> IncludeUser(this IQueryable<TeamMember> query)
    {
        return query.Include(m => m.User);
    }
    
    public static IQueryable<TeamMember> IncludeTeam(this IQueryable<TeamMember> query)
    {
        return query.Include(m => m.Team);
    }
}