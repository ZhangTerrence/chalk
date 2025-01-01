namespace chalk.Server.DTOs.Requests;

public record SendInviteRequest(long UserId, long OrganizationId, long OrganizationRoleId);