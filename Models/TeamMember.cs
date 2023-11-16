namespace Kanban.Models;

public enum Statuses
{
    Left,
    Member,
    Kicked
}

public enum AccessLevels
{
    Read,
    ReadWrite,
    Admin
}

public class TeamMember
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TeamId { get; set; }
    public Statuses Status { get; set; }
    public AccessLevels AccessLevel { get; set; }
    public User? User { get; set; }
    public Team? Team { get; set; }
}