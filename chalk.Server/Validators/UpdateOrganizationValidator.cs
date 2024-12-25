using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateOrganizationValidator : AbstractValidator<UpdateOrganizationRequest>
{
    public UpdateOrganizationValidator()
    {
        RuleFor(e => e.Name)
            .Length(3, 31)
            .WithMessage("Name property must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description property must have at most 255 characters.");
    }
}