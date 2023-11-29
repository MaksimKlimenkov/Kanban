using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class BoardColumn
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Title { get; set; } = null!;
    public int BoardId { get; set; }
    public Board? Board { get; set; }
}