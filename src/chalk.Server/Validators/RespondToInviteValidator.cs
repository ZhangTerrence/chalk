using chalk.Server.Common.Errors;
using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class RespondToInviteValidator : AbstractValidator<RespondToInviteRequest>
{
    public RespondToInviteValidator()
    {
        RuleFor(e => e.InviteType)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("InviteType"))
            .IsInEnum()
            .WithMessage(Errors.Validation.IsInvalid("InviteType"));
        RuleFor(e => e.UserId)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("UserId"));
        RuleFor(e => e.OrganizationId)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("OrganizationId"));
        RuleFor(e => e.Accept)
            .NotEmpty()
            .WithMessage(Errors.Validation.IsRequired("Accept"));
    }
}