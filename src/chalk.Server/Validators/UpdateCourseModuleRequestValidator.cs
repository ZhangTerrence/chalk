using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateCourseModuleRequestValidator : AbstractValidator<UpdateCourseModuleRequest>
{
    public UpdateCourseModuleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The course module's name is required.")
            .Length(3, 31)
            .WithMessage("The course module's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The course module's description must have at most 255 characters.");
        RuleFor(e => e.CourseId)
            .NotNull()
            .WithMessage("Must specify the course which the module belongs to.");
    }
}