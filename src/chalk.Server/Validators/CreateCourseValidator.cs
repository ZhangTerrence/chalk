using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateCourseValidator : AbstractValidator<CreateCourseRequest>
{
    public CreateCourseValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The course's name is required.")
            .Length(3, 31)
            .WithMessage("The course's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The course's description must have between 3 and 255 characters.");
        RuleFor(e => e.PreviewImage);
        RuleFor(e => e.Code)
            .MaximumLength(31)
            .WithMessage("The course's code must have between 3 and 31 characters.");
        RuleFor(e => e.Public)
            .NotNull()
            .WithMessage("Must specify whether the course is public.");
        RuleFor(e => e.OrganizationId);
    }
}