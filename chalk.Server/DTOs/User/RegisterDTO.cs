namespace chalk.Server.DTOs.User;

public record RegisterDTO
{
    public RegisterDTO(string firstName, string lastName, string displayName, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        DisplayName = displayName;
        Email = email;
        Password = password;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string DisplayName { get; }
    public string Email { get; }
    public string Password { get; }
}