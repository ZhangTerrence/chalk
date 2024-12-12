namespace chalk.Server.DTOs;

public record RegisterDTO(string FirstName, string LastName, string DisplayName, string Email, string Password)
{
}