namespace PROJETO.DTO.Request.Auth;

public class RegisterRequest
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public DateTime BirthDay { get; set; }
}
