using chalk.Server.DTOs.Requests;
using FluentValidation;

namespace chalk.Server.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
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
        RuleFor(e => e.Email)
            .EmailAddress()
            .WithMessage("The user's email is invalid.");
        RuleFor(e => e.Password)
            .Matches("^(?:(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[-._@+]).*).{8,}$")
            .WithMessage(
                "The user's password must have at least 8 characters with least one number, one lowercase letter, one upper case letter, one special character.");
    }
}