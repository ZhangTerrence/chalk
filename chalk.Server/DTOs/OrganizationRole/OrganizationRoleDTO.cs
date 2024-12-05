namespace chalk.Server.DTOs.OrganizationRole;

public record OrganizationRoleDTO
{
    public OrganizationRoleDTO(Entities.OrganizationRole organizationRole)
    {
        Id = organizationRole.Id;
        Name = organizationRole.Name;
        Description = organizationRole.Description;
        Permissions = organizationRole.Permissions;
        CreatedDate = organizationRole.CreatedDate;
        UpdatedDate = organizationRole.UpdatedDate;
    }

    public long Id { get; }
    public string Name { get; }
    public string? Description { get; }
    public long Permissions { get; }
    public DateTime CreatedDate { get; }
    public DateTime UpdatedDate { get; }
}