using chalk.Server.DTOs;
using FluentValidation;

namespace chalk.Server.Validators;

public class SendInviteValidator : AbstractValidator<SendInviteDTO>
{
    public SendInviteValidator()
    {
        RuleFor(e => e.UserId)
            .NotEmpty()
            .WithMessage("UserId property is required.");
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage("OrganizationId property is required.");
        RuleFor(e => e.OrganizationRoleId)
            .NotEmpty()
            .WithMessage("OrganizationRoleId property is required.");
    }
}