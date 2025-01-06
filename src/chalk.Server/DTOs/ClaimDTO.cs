namespace chalk.Server.DTOs;

public record ClaimDTO(
    long UserId,
    string DisplayName,
    IEnumerable<string> Roles
);