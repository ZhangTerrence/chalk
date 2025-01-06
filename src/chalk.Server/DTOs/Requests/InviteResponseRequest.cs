using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record InviteResponseRequest(
    Origin? Origin,
    long? UserId,
    long? CourseId,
    long? OrganizationId,
    bool? AcceptInvite
);