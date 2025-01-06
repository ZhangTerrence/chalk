using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseValidator()
    {
        RuleFor(e => e.Name)
            .Length(3, 31)
            .WithMessage("The course's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The course's description must have at most 255 characters.");
        RuleFor(e => e.PreviewImage);
        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("The course's code must have at most 31 characters.");
        RuleFor(e => e.Public);
    }
}