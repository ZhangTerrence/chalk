namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to invite a user to either a course or organization.
/// </summary>
/// <param name="ReceiverId">The id of the user to be invited.</param>
/// <param name="RoleId">The id of the role the user will be assigned if they accept.</param>
/// <remarks><paramref name="ReceiverId"/>, and <paramref name="RoleId"/> are required.</remarks>
public record InviteRequest(
    long? ReceiverId,
    long? RoleId
);