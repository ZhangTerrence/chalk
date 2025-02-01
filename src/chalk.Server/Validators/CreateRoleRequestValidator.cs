using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The role's name is required.")
            .Length(3, 31)
            .WithMessage("The role's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The role's description must have at most 255 characters.");
        RuleFor(e => e.Permissions)
            .NotNull()
            .WithMessage("Must specify the role's permissions.");
        RuleFor(e => e.RelativeRank)
            .NotNull()
            .WithMessage("Must specify the role's rank.")
            .GreaterThanOrEqualTo(0).When(e => e.RelativeRank.HasValue)
            .WithMessage("The role's relative rank must be zero or positive.");
    }
}