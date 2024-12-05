using chalk.Server.DTOs.Organization;
using FluentValidation;

namespace chalk.Server.Validators.Organization;

public class SendInviteValidator : AbstractValidator<SendInviteDTO>
{
    public SendInviteValidator()
    {
        RuleFor(e => e.UserId)
            .Must(userId => userId > 0)
            .WithMessage("UserId is required.");

        RuleFor(e => e.OrganizationId)
            .Must(organizationId => organizationId > 0)
            .WithMessage("OrganizationId is required.");

        RuleFor(e => e.OrganizationRoleId)
            .Must(organizationRoleId => organizationRoleId > 0)
            .WithMessage("OrganizationRoleId is required.");
    }
}