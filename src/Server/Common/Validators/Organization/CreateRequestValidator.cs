using FluentValidation;
using Server.Common.Requests.Organization;

namespace Server.Common.Validators.Organization;

internal class CreateRequestValidator : AbstractValidator<CreateRequest>
{
  public CreateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The organization's name is required.")
      .Length(3, 31)
      .WithMessage("The organization's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The organization's description must have at most 255 characters.");
    this.RuleFor(e => e.IsPublic)
      .NotNull()
      .WithMessage("Must specify whether the course is public.");
  }
}
