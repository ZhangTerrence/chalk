namespace chalk.Server.DTOs.User;

public record UserDTO
{
    public UserDTO(Entities.User user)
    {
        Id = user.Id;
        Email = user.Email!;
        DisplayName = user.DisplayName;
        CreatedDate = user.CreatedDate;
        UpdatedDate = user.UpdatedDate;
    }

    public Guid Id { get; init; }
    public string Email { get; init; }
    public string DisplayName { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
}