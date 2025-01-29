using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class InviteRequestValidator : AbstractValidator<InviteRequest>
{
    public InviteRequestValidator()
    {
        RuleFor(e => e.ReceiverId)
            .NotNull()
            .WithMessage("Must specify the user the invite is addressed to.");
        RuleFor(e => e.RoleId)
            .NotNull()
            .WithMessage("Must specify the role the user will be assigned if they accept.");
    }
}