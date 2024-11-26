using chalk.Server.DTOs.Course;
using FluentValidation;

namespace chalk.Server.Validators.Course;

public class CreateCourseValidator : AbstractValidator<CreateCourseDTO>
{
    public CreateCourseValidator()
    {
        RuleFor(e => e.Name)
            .NotNull()
            .WithMessage("Name is required.")
            .Length(3, 31)
            .WithMessage("Name must be between 3 and 31 characters.");

        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("Code must be at most 31 characters.");

        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("Description must be at most 255 characters.");

        RuleFor(e => e.OrganizationId)
            .NotNull()
            .WithMessage("Organization id is required.");
    }
}