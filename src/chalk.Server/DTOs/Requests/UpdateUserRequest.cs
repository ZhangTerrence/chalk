namespace chalk.Server.DTOs.Requests;

public record UpdateUserRequest(
    string? FirstName,
    string? LastName,
    string? DisplayName,
    string? Description,
    string? ProfilePicture
);