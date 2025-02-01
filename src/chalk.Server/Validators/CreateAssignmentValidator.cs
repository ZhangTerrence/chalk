using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateAssignmentValidator : AbstractValidator<CreateAssignmentRequest>
{
    public CreateAssignmentValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The assignment group's name is required.")
            .Length(3, 31)
            .WithMessage("The assignment group's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The assignment group's description must have at most 255 characters.");
        RuleFor(e => e.IsOpen)
            .NotNull()
            .WithMessage("Whether specify whether the assignment is open for submissions.");
        RuleFor(e => e.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).When(e => e.DueDate.HasValue)
            .WithMessage("The assignment's due date must be after the current date.");
        RuleFor(e => e.AllowedAttempts)
            .GreaterThanOrEqualTo(1).When(e => e.AllowedAttempts.HasValue)
            .WithMessage("The assignment's maximum allowed attempts must be greater than or equal to 1.");
    }
}