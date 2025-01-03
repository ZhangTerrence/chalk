using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateOrganizationValidator : AbstractValidator<CreateOrganizationRequest>
{
    public CreateOrganizationValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(3, 31)
            .WithMessage("Name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description must have at most 255 characters.");
        RuleFor(e => e.OwnerId)
            .NotEmpty()
            .WithMessage("Owner id is required.");
    }
}