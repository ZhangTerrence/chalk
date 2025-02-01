using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class ReorderModulesRequestValidator : AbstractValidator<ReorderModulesRequest>
{
    public ReorderModulesRequestValidator()
    {
        RuleFor(x => x.Modules)
            .NotEmpty()
            .WithMessage("At least one module must be specified before reordering.");
    }
}