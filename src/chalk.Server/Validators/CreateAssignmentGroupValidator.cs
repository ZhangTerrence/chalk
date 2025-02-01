using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateAssignmentGroupValidator : AbstractValidator<CreateAssignmentGroupRequest>
{
    public CreateAssignmentGroupValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The assignment group's name is required.")
            .Length(3, 31)
            .WithMessage("The assignment group's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The assignment group's description must have at most 255 characters.");
        RuleFor(e => e.Weight)
            .NotNull()
            .WithMessage("The assignment group's weight is required.")
            .LessThanOrEqualTo(100).When(e => e.Weight.HasValue)
            .WithMessage("The assignment group's weight must be less than 100%.");
    }
}