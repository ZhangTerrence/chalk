using chalk.Server.DTOs.Organization;
using chalk.Server.Entities;

namespace chalk.Server.Extensions;

public static class OrganizationExtension
{
    public static Organization ToOrganization(this CreateOrganizationDTO createOrganizationDTO, User owner)
    {
        return new Organization
        {
            Id = new Guid(),
            Name = createOrganizationDTO.Name,
            Description = createOrganizationDTO.Description,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            OwnerId = owner.Id,
            Owner = owner
        };
    }

    public static OrganizationDTO ToOrganizationResponseDTO(this Organization organization)
    {
        return new OrganizationDTO(organization);
    }
}