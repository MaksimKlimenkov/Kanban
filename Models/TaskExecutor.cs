using System.ComponentModel.DataAnnotations.Schema;

namespace Kanban.Models;

public class TaskExecutor
{
    public int TaskId { get; set; }
    public int TeamMemberId { get; set; }
    public BoardTask? Task { get; set; }
    public TeamMember? TeamMember { get; set; }
}