using FluentValidation;
using Server.Common.Requests.Module;

namespace Server.Common.Validators.Module;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
  public UpdateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The module's name is required.")
      .Length(3, 31)
      .WithMessage("The module's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The module's description must have at most 255 characters.");
  }
}