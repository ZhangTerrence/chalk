using FluentValidation;
using Server.Common.Requests.Assignment;

namespace Server.Common.Validators.Assignment;

internal class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
  public UpdateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The assignment group's name is required.")
      .Length(3, 31)
      .WithMessage("The assignment group's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The assignment group's description must have at most 255 characters.");
    this.RuleFor(e => e.DueOnUtc)
      .GreaterThanOrEqualTo(DateTime.UtcNow).When(e => e.DueOnUtc.HasValue)
      .WithMessage("The assignment's due date must be after the current date.");
  }
}
