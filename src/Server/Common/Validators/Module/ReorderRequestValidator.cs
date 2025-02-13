using FluentValidation;
using Server.Common.Requests.Module;

namespace Server.Common.Validators.Module;

internal class ReorderRequestValidator : AbstractValidator<ReorderRequest>
{
  public ReorderRequestValidator()
  {
    this.RuleFor(e => e.CourseId)
      .NotEmpty()
      .WithMessage("Must specify the id of the course the modules belong to.");
    this.RuleFor(e => e.Modules)
      .NotEmpty()
      .WithMessage("At least one module must be specified before reordering.");
  }
}
