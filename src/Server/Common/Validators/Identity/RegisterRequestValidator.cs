using FluentValidation;
using Server.Common.Requests.Identity;

namespace Server.Common.Validators.Identity;

internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
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
    this.RuleFor(e => e.Email)
      .NotEmpty()
      .WithMessage("The user's email is required.")
      .EmailAddress()
      .WithMessage("The user's email is invalid.");
    this.RuleFor(e => e.Password)
      .NotEmpty()
      .WithMessage("The user's password is required.")
      .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*).{8,}$")
      .WithMessage(
        "The user's password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
  }
}
