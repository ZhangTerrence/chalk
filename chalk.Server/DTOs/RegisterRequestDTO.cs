namespace chalk.Server.DTOs;

public record RegisterRequestDTO
{
    public RegisterRequestDTO(string displayName, string email, string password)
    {
        DisplayName = displayName;
        Email = email;
        Password = password;
    }

    public string DisplayName { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}