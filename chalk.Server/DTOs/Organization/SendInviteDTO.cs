namespace chalk.Server.DTOs.Organization;

public record SendInviteDTO
{
    public SendInviteDTO(long userId, long organizationId, long organizationRoleId)
    {
        UserId = userId;
        OrganizationId = organizationId;
        OrganizationRoleId = organizationRoleId;
    }

    public long UserId { get; }
    public long OrganizationId { get; }
    public long OrganizationRoleId { get; }
}