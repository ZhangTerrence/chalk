using FluentValidation;
using Server.Common.Requests.AssignmentGroup;

namespace Server.Common.Validators.AssignmentGroup;

public class CreateRequestValidator : AbstractValidator<CreateRequest>
{
  public CreateRequestValidator()
  {
    this.RuleFor(e => e.CourseId)
      .NotEmpty()
      .WithMessage("Must specify the id of the course the assignment group belongs to.");
    this.RuleFor(e => e.Name)
      .NotEmpty()
      .WithMessage("The assignment group's name is required.")
      .Length(3, 31)
      .WithMessage("The assignment group's name must have between 3 and 31 characters.");
    this.RuleFor(e => e.Description)
      .MaximumLength(255)
      .WithMessage("The assignment group's description must have at most 255 characters.");
    this.RuleFor(e => e.Weight)
      .NotNull()
      .WithMessage("The assignment group's weight is required.")
      .GreaterThanOrEqualTo(0).When(e => e.Weight.HasValue)
      .WithMessage("The assignment group's weight must be greater than or equal to 0%.")
      .LessThanOrEqualTo(100).When(e => e.Weight.HasValue)
      .WithMessage("The assignment group's weight must be less than or equal to 100%.");
  }
}
