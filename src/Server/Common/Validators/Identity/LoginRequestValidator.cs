using FluentValidation;
using Server.Common.Requests.Identity;

namespace Server.Common.Validators.Identity;

internal class LoginRequestValidator : AbstractValidator<LoginRequest>
{
  public LoginRequestValidator()
  {
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
