using FluentValidation;
using Server.Common.Requests.Course;

namespace Server.Common.Validators.Course;

internal class UpdateRequestValidator : AbstractValidator<UpdateRequest>
{
  public UpdateRequestValidator()
  {
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The course's name is required.")
      .Length(3, 31)
      .WithMessage("The course's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Code)
      .MaximumLength(31)
      .WithMessage("The course's code must have at most 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The course's description must have at most 255 characters.");
    this.RuleFor(e => e.Image);
    this.RuleFor(e => e.IsPublic)
      .NotNull()
      .WithMessage("Must specify whether the course is public.");
  }
}
