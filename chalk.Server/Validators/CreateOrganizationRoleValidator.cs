using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateOrganizationRoleValidator : AbstractValidator<CreateOrganizationRoleDTO>
{
    public CreateOrganizationRoleValidator()
    {
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage("OrganizationId property is required.");
        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage("Name property is required.")
            .Length(1, 31)
            .WithMessage("Name property must have between 1 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description property must have between 1 and 255 characters.");
        RuleFor(e => e.Permissions)
            .NotEmpty()
            .WithMessage("Permissions property is required.");
    }
}