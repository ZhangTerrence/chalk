namespace chalk.Server.DTOs.User;

public record LoginDTO
{
    public LoginDTO(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; init; }
    public string Password { get; init; }
}