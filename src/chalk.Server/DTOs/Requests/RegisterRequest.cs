namespace chalk.Server.DTOs.Requests;

public record RegisterRequest(
    string? FirstName,
    string? LastName,
    string? DisplayName,
    string? Email,
    string? Password
);