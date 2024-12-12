using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
{
    public CreateCourseValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage("Name property is required.")
            .Length(3, 31)
            .WithMessage("Name property must have between 3 and 31 characters.");
        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("Code property must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description property must have between 3 and 255 characters.");
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage("OrganizationId property is required.");
    }
}