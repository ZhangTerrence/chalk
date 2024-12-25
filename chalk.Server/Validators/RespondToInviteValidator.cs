using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class RespondToInviteValidator : AbstractValidator<RespondToInviteRequest>
{
    public RespondToInviteValidator()
    {
        RuleFor(e => e.InviteType)
            .NotNull()
            .WithMessage("InviteType property is required.")
            .IsInEnum()
            .WithMessage("InviteType property is invalid.");
        RuleFor(e => e.UserId)
            .NotEmpty()
            .WithMessage("UserId property is required.");
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage("OrganizationId property is required.");
        RuleFor(e => e.Accept)
            .NotNull()
            .WithMessage("Accept property is required.");
    }
}