using FluentValidation;
using Server.Common.Requests.File;

namespace Server.Common.Validators.File;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
  public CreateRequestValidator()
  {
    this.RuleFor(e => e.For)
      .NotNull()
      .WithMessage("Must specify whether the file is for either a module, assignment, or submission.")
      .IsInEnum()
      .WithMessage("Invalid choice.");
    this.RuleFor(e => e.EntityId)
      .NotNull()
      .WithMessage("Must specify the id of the module, assignment, or submission.");
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The file's name is required.")
      .Length(3, 31)
      .WithMessage("The file's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The file's description must have at most 255 characters.");
    this.RuleFor(e => e.File)
      .NotEmpty()
      .WithMessage("The file is required.");
  }
}