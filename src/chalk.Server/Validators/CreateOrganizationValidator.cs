using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The organization's name is required.")
            .Length(3, 31)
            .WithMessage("The organization's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The organization's description must have at most 255 characters.");
        RuleFor(e => e.OwnerId)
            .NotEmpty()
            .WithMessage("Must specify the user the organization belongs to.");
    }
}