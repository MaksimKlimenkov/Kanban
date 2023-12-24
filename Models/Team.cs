using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class Team
{
    public int Id { get; set; }
    [MaxLength(30)]
    public string Title { get; set; } = null!;
    public string OwnerId { get; set; } = null!;
    public User? Owner { get; set; }
    public List<TeamMember> Members { get; set; } = new();
}