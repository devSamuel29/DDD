namespace PROJETO.Infra.Models;

public class UserModel
{
    public int Id { get; set; }

    public string Role { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
