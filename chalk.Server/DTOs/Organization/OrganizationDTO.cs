using chalk.Server.DTOs.User;
using chalk.Server.Extensions;

namespace chalk.Server.DTOs.Organization;

public class OrganizationDTO
{
    public OrganizationDTO(Entities.Organization organization)
    {
        Id = organization.Id;
        Name = organization.Name;
        Description = organization.Description;
        Owner = organization.Owner.ToUserResponseDTO();
        CreatedDate = organization.CreatedDate;
        UpdatedDate = organization.UpdatedDate;
    }

    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public UserDTO Owner { get; init; }
    public DateTime CreatedDate { get; init; }
    public DateTime UpdatedDate { get; init; }
}