namespace Kanban.Models;

public class TaskComment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int TeamMemberId { get; set; }
    public string Comment { get; set; } = null!;
    public BoardTask? Task { get; set; }
    public TeamMember? TeamMember { get; set; }
}