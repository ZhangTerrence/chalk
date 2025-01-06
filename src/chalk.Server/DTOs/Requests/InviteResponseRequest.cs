using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record InviteResponseRequest(
    InviteType? InviteType,
    long? UserId,
    long? CourseId,
    long? OrganizationId,
    bool? AcceptInvite
);