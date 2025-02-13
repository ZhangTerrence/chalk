using FluentValidation;
using Server.Common.Requests.User;

namespace Server.Common.Validators.User;

internal class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
  public UpdateRequestValidator()
  {
    this.RuleFor(e => e.FirstName)
      .NotEmpty()
      .WithMessage("The user's first name is required.")
      .Length(1, 31)
      .WithMessage("The user's first name must have between 1 and 31 characters.");
    this.RuleFor(e => e.LastName)
      .NotEmpty()
      .WithMessage("The user's last name is required.")
      .Length(1, 31)
      .WithMessage("The user's last name must have between 1 and 31 characters.");
    this.RuleFor(e => e.DisplayName)
      .NotEmpty()
      .WithMessage("The user's display name is required.")
      .Length(3, 31)
      .WithMessage("The user's display name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The user's description must have at most 255 characters.");
    this.RuleFor(e => e.Image);
  }
}
