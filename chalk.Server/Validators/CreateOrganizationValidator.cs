using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage("Name property is required.")
            .Length(3, 31)
            .WithMessage("Name property must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description property must have at most 255 characters.");
        RuleFor(e => e.OwnerId)
            .NotEmpty()
            .WithMessage("OwnerId property is required.");
    }
}