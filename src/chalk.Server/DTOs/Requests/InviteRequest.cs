using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record InviteRequest(
    InviteType? InviteType,
    long? UserId,
    long? CourseId,
    long? OrganizationId,
    long? CourseRoleId,
    long? OrganizationRoleId
);