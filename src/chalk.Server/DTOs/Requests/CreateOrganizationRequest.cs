namespace chalk.Server.DTOs.Requests;

public record CreateOrganizationRequest(
    string? Name,
    string? Description,
    string? ProfilePicture,
    long? OwnerId
);