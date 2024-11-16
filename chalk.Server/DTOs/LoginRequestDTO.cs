namespace chalk.Server.DTOs;

public record LoginRequestDTO
{
    public LoginRequestDTO(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; init; }
    public string Password { get; init; }
}