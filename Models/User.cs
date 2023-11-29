using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Kanban.Models;

public class User : IdentityUser
{
    [MaxLength(15)]
    public string FirstName { get; set; } = null!;
    [MaxLength(20)]
    public string LastName { get; set; } = null!;
}