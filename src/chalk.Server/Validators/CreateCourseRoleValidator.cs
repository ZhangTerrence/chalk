using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateCourseRoleValidator : AbstractValidator<CreateCourseRoleRequest>
{
    public CreateCourseRoleValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("The role's name is required.")
            .Length(3, 31)
            .WithMessage("The role's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The role's description must have between 3 and 255 characters.");
        RuleFor(e => e.Permissions)
            .NotNull()
            .WithMessage("Must specify the role's permissions.");
        RuleFor(e => e.Rank)
            .NotNull()
            .WithMessage("Must specify the role's rank.");
        RuleFor(e => e.CourseId)
            .NotNull()
            .WithMessage("Must specify the course which the role belongs to.");
    }
}