using Kanban.Models;

namespace Kanban.Dto;

public class TeamMemberDto
{
    public string UserId { get; set; } = null!;
    public int TeamId { get; set; }
    public Statuses Status { get; set; }
    public AccessLevels AccessLevel { get; set; }
}