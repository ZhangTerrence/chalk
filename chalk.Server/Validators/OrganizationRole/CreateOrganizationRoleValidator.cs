using chalk.Server.DTOs.OrganizationRole;
using FluentValidation;

namespace chalk.Server.Validators.OrganizationRole;

public class CreateOrganizationRoleValidator : AbstractValidator<CreateOrganizationRoleDTO>
{
    public CreateOrganizationRoleValidator()
    {
        RuleFor(e => e.OrganizationId)
            .Must(organizationId => organizationId > 0)
            .WithMessage("OrganizationId is required.");

        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage("Name is required.")
            .Length(1, 31)
            .WithMessage("Name must be between 1 and 31 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description must be at most 255 characters.");

        RuleFor(e => e.Permissions)
            .Must(permissions => permissions > 0)
            .WithMessage("Permissions are required.");
    }
}