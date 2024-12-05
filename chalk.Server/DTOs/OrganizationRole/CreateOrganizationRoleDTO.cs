namespace chalk.Server.DTOs.OrganizationRole;

public record CreateOrganizationRoleDTO
{
    public CreateOrganizationRoleDTO(long organizationId, string name, string? description, long permissions)
    {
        OrganizationId = organizationId;
        Name = name;
        Description = description;
        Permissions = permissions;
    }

    public long OrganizationId { get; set; }
    public string Name { get; }
    public string? Description { get; }
    public long Permissions { get; }
}