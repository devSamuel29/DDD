namespace PROJETO.Infra.Identity;

public class Claims
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Role { get; set; }

    public int Exp { get; set; }
}