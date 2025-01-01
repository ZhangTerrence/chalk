using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class RespondToInviteValidator : AbstractValidator<RespondToInviteRequest>
{
    public RespondToInviteValidator()
    {
        RuleFor(e => e.Invite)
            .NotEmpty()
            .WithMessage("Invite type is required")
            .IsInEnum()
            .WithMessage("Invite type is invalid.");
        RuleFor(e => e.UserId)
            .NotEmpty()
            .WithMessage("User id is required.");
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage("Organization id is required.");
        RuleFor(e => e.Accept)
            .NotEmpty()
            .WithMessage("Accept is required.");
    }
}