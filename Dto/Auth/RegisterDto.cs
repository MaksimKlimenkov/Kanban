namespace Kanban.Dto.Auth;

public class RegisterDto : UserDto
{
    [Password(MinLength = 8, MaxLength = 20, EnforceUppercase = true, EnforceNumbers = true)]
    public string Password { get; set; } = null!;
    public string PasswordConfirm { get; set; } = null!;
}