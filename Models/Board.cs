using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class Board
{
    public int Id { get; set; }
    [MaxLength(30)]
    public string Title { get; set; } = null!;
    public int TeamId { get; set; }
    public Team? Team { get; set; }
}