using chalk.Server.DTOs.Requests;
using chalk.Server.Shared;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
    public CreateRoleRequestValidator()
    {
        RuleFor(e => e.Origin)
            .NotNull()
            .WithMessage("Must specify whether the role is for a course or an organization.")
            .IsInEnum()
            .WithMessage("The origin is invalid.");
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
            .Must(e => e!.Value >= 0)
            .WithMessage("The role's relative rank must be zero or positive.");
        RuleFor(e => e.CourseId)
            .NotNull().When(e => e.Origin == RoleOrigin.Course)
            .WithMessage("Must specify the course which the role belongs to.");
        RuleFor(e => e.OrganizationId)
            .NotNull().When(e => e.Origin == RoleOrigin.Organization)
            .WithMessage("Must specify the organization which the role belongs to.");
    }
}