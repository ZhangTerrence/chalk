using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

public record RespondToInviteRequest(Invite Invite, long UserId, long OrganizationId, bool Accept);