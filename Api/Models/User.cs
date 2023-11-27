using System.ComponentModel.DataAnnotations;

namespace Kanban.Models;

public class User
{
    public int Id { get; set; }
    [EmailAddress]
    public string Email { get; set; } = null!;
    [MaxLength(35)]
    public string Username { get; set; } = null!;
    [MaxLength(15)]
    public string FirstName { get; set; } = null!;
    [MaxLength(20)]
    public string LastName { get; set; } = null!;
    [MaxLength(80)]
    public string PasswordHash { get; set; } = null!;
}