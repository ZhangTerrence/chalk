using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateFileRequestValidator : AbstractValidator<CreateFileRequest>
{
    public CreateFileRequestValidator()
    {
        RuleFor(e => e.For)
            .NotNull()
            .WithMessage("Must specify whether the file is for either a module, assignment, or submission.")
            .IsInEnum()
            .WithMessage("Invalid choice.");
        RuleFor(e => e.EntityId)
            .NotNull()
            .WithMessage("Must specify the id of the module, assignment, or submission.");
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The file's name is required.")
            .Length(3, 31)
            .WithMessage("The file's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The file's description must have at most 255 characters.");
        RuleFor(e => e.File)
            .NotEmpty()
            .WithMessage("The file is required.");
    }
}