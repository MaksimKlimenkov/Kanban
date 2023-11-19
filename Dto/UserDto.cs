namespace Kanban.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    // public string LastName { get; set; } = null!;
}