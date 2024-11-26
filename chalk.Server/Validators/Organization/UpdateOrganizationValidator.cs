using chalk.Server.DTOs.Organization;
using FluentValidation;

namespace chalk.Server.Validators.Organization;

public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationDTO>
{
    public UpdateOrganizationValidator()
    {
        RuleFor(e => e.Name)
            .Length(3, 31)
            .WithMessage("Name must be between 3 and 31 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description must be at most 255 characters.");
    }
}