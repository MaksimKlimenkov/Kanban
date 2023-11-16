namespace Kanban.Models;

public class TaskExecutor
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public BoardTask? Task { get; set; }
    public User? User { get; set; }
}