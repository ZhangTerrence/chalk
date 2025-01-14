using chalk.Server.DTOs.Requests;
using chalk.Server.Shared;
using FluentValidation;

namespace chalk.Server.Validators;

public class InviteRequestValidator : AbstractValidator<InviteRequest>
{
    public InviteRequestValidator()
    {
        RuleFor(e => e.Origin)
            .NotNull()
            .WithMessage("Must specify whether the invite is from a course or an organization.")
            .IsInEnum()
            .WithMessage("The origin is invalid.");
        RuleFor(e => e.UserId)
            .NotNull()
            .WithMessage("Must specify the user the invite is addressed to.");
        RuleFor(e => e.CourseId)
            .NotNull().When(e => e.Origin == Origin.Course)
            .WithMessage("Must specify the course the invite is addressed from.");
        RuleFor(e => e.OrganizationId)
            .NotNull().When(e => e.Origin == Origin.Organization)
            .WithMessage("Must specify the organization the invite is addressed from.");
        RuleFor(e => e.RoleId)
            .NotNull()
            .WithMessage("Must specify the role the user will be assigned if they accept.");
    }
}