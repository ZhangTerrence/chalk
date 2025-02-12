using FluentValidation;
using Server.Common.Requests.Role;

namespace Server.Common.Validators.Role;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
  public CreateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The role's name is required.")
      .Length(3, 31)
      .WithMessage("The role's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The role's description must have at most 255 characters.");
    this.RuleFor(e => e.Permissions)
      .NotNull()
      .WithMessage("Must specify the role's permissions.");
    this.RuleFor(e => e.RelativeRank)
      .NotNull()
      .WithMessage("Must specify the role's rank.")
      .GreaterThanOrEqualTo(0).When(e => e.RelativeRank.HasValue)
      .WithMessage("The role's relative rank must be zero or positive.");
  }
}