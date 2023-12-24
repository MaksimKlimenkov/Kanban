namespace Kanban.Dto;

public class TeamMemberDto
{
    public string UserId { get; set; } = null!;
    public int TeamId { get; set; }
    public required string Status { get; set; }
    public required string AccessLevel { get; set; }
}