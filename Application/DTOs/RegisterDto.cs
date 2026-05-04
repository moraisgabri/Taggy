namespace Taggy.Application.DTOs;

public class RegisterDto
{
    required public string Name { get; set; }
    required public string Email { get; set; }
    required public string Password { get; set; }
}
