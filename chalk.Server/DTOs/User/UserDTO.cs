namespace chalk.Server.DTOs.User;

public record UserDTO
{
    public UserDTO(Entities.User user)
    {
        Id = user.Id;

        Email = user.Email;
        FullName = user.FullName;
        DisplayName = user.DisplayName;
        ProfilePicture = user.ProfilePicture;
        CreatedDate = user.CreatedDate;
        UpdatedDate = user.UpdatedDate;
        Organizations = user.UserOrganizations
            .Select(e => new OrganizationDTO(e.Organization.Id, e.Organization.Name))
            .ToList();
        Courses = user.UserCourses
            .Select(e => new CourseDTO(e.Course.Id, e.Course.Name, e.Course.Code))
            .ToList();
        Channels = user.ChannelParticipants
            .Select(e => new ChannelDTO(e.Channel.Id, e.Channel.Name))
            .ToList();
    }

    public long Id { get; init; }
    public string FullName { get; init; }
    public string? Email { get; init; }
    public string DisplayName { get; init; }
    public string? ProfilePicture { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
    public IEnumerable<OrganizationDTO> Organizations { get; init; }
    public IEnumerable<CourseDTO> Courses { get; init; }
    public IEnumerable<ChannelDTO> Channels { get; init; }

    public sealed record OrganizationDTO(long OrganizationId, string Name);

    public sealed record CourseDTO(long CourseId, string Name, string Code);

    public sealed record ChannelDTO(long ChannelId, string Name);
}