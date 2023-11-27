using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class BoardTask
{
    public int Id { get; set; }
    public int ColumnId { get; set; }
    [MaxLength(64)]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? DeadLine { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public BoardColumn? BoardColumn { get; set; }
    public List<TaskExecutor> Executors { get; set; } = new();
}