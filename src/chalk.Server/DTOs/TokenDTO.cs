namespace chalk.Server.DTOs;

public record TokenDTO(
    string Issuer,
    string Audience,
    string Key
);