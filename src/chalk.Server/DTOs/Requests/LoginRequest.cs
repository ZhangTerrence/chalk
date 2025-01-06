namespace chalk.Server.DTOs.Requests;

public record LoginRequest(
    string? Email,
    string? Password
);