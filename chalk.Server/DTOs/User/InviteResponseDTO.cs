using chalk.Server.Common;

namespace chalk.Server.DTOs.User;

public record InviteResponseDTO
{
    public InviteResponseDTO(InviteType inviteType, long userId, long organizationId, bool accept)
    {
        InviteType = inviteType;
        UserId = userId;
        OrganizationId = organizationId;
        Accept = accept;
    }

    public InviteType InviteType { get; set; }
    public long UserId { get; }
    public long OrganizationId { get; }
    public bool Accept { get; }
}