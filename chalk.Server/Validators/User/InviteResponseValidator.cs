using chalk.Server.DTOs.User;
using FluentValidation;

namespace chalk.Server.Validators.User;

public class InviteResponseValidator : AbstractValidator<InviteResponseDTO>
{
    public InviteResponseValidator()
    {
        RuleFor(e => e.InviteType)
            .NotNull()
            .WithMessage("Invite type is required.")
            .IsInEnum()
            .WithMessage("Invite type is invalid.");

        RuleFor(e => e.UserId)
            .Must(userId => userId > 0)
            .WithMessage("UserId is required.");

        RuleFor(e => e.OrganizationId)
            .Must(organizationId => organizationId > 0)
            .WithMessage("OrganizationId is required.");

        RuleFor(e => e.Accept)
            .NotNull()
            .WithMessage("Accept is required.");
    }
}