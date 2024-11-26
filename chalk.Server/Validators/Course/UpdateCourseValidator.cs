using chalk.Server.DTOs.Course;
using FluentValidation;

namespace chalk.Server.Validators.Course;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseDTO>
{
    public UpdateCourseValidator()
    {
        RuleFor(e => e.Name)
            .Length(3, 31)
            .WithMessage("Name must be between 3 and 31 characters.");

        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("Code must be at most 31 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description must be at most 255 characters.");
    }
}