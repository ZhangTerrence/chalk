using chalk.Server.Common;

namespace chalk.Server.DTOs;

public record RespondToInviteDTO(InviteType InviteType, long UserId, long OrganizationId, bool Accept)
{
}