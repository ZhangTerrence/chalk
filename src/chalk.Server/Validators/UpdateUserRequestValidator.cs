using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(e => e.FirstName)
            .Length(1, 31)
            .WithMessage("The user's first name must have between 1 and 31 characters.");
        RuleFor(e => e.LastName)
            .Length(1, 31)
            .WithMessage("The user's last name must have between 1 and 31 characters.");
        RuleFor(e => e.DisplayName)
            .Length(3, 31)
            .WithMessage("The user's display name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The user's description must have at most 255 characters.");
        RuleFor(e => e.ProfilePicture);
    }
}