using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record InviteRequest(
    Origin? Origin,
    long? UserId,
    long? CourseId,
    long? OrganizationId,
    long? CourseRoleId,
    long? OrganizationRoleId
);