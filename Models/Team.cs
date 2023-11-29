using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class Team
{
    public int Id { get; set; }
    [MaxLength(30)]
    public string Title { get; set; } = null!;
    public int OwnerId { get; set; }
    public User? Owner { get; set; }
}