namespace Kanban.Dto.Auth;

public class RegisterDto : UserDto
{
    public string Password { get; set; } = null!;
    public string PasswordConfirm { get; set; } = null!;
}