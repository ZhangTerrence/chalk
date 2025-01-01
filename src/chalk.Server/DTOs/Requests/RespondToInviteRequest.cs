using chalk.Server.Common;

namespace chalk.Server.DTOs.Requests;

public record RespondToInviteRequest(InviteType InviteType, long UserId, long OrganizationId, bool Accept);