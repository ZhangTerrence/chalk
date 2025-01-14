using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to invite a user to either a course or organization.
/// </summary>
/// <param name="Origin">Whether it is for a course or an organization.</param>
/// <param name="UserId">The id of the user to be invited.</param>
/// <param name="CourseId">The id of the course the user is invited to.</param>
/// <param name="OrganizationId">The id of the organization the user is invited to.</param>
/// <param name="RoleId">The id of the role the user will be assigned if they accept.</param>
/// <remarks>
///     <paramref name="Origin"/>, <paramref name="UserId"/>, and <paramref name="RoleId"/> are required.
///     <paramref name="CourseId"/> and <paramref name="OrganizationId"/> are conditionally required based on <paramref name="Origin"/>.
/// </remarks>
public record InviteRequest(
    Origin? Origin,
    long? UserId,
    long? CourseId,
    long? OrganizationId,
    long? RoleId
);