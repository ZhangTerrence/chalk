using FluentValidation;
using Server.Common.Requests.Organization;

namespace Server.Common.Validators.Organization;

public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
  public UpdateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The organization's name is required.")
      .Length(3, 31)
      .WithMessage("The organization's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The organization's description must have at most 255 characters.");
    this.RuleFor(e => e.Image);
    this.RuleFor(e => e.IsPublic)
      .NotNull()
      .WithMessage("Must specify whether the organization is public.");
  }
}