using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class User
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Email { get; set; } = null!;
    [MaxLength(35)]
    public string Username { get; set; } = null!;
    [MaxLength(15)]
    public string FirstName { get; set; } = null!;
    [MaxLength(20)]
    public string LastName { get; set; } = null!;
}