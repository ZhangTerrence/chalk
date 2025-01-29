using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateModuleRequestValidator : AbstractValidator<UpdateModuleRequest>
{
    public UpdateModuleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The module's name is required.")
            .Length(3, 31)
            .WithMessage("The module's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The module's description must have at most 255 characters.");
    }
}