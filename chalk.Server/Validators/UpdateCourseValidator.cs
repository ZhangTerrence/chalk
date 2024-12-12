using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDTO>
{
    public UpdateCourseValidator()
    {
        RuleFor(e => e.Name)
            .Length(3, 31)
            .WithMessage("Name property must have between 3 and 31 characters.");
        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("Code property must have at most 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description property must have at most 255 characters.");
    }
}