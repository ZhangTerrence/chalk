using chalk.Server.DTOs.Requests;
using chalk.Server.Shared;
using FluentValidation;

namespace chalk.Server.Validators;

public class CreateAttachmentRequestValidator : AbstractValidator<CreateAttachmentRequest>
{
    public CreateAttachmentRequestValidator()
    {
        RuleFor(e => e.Origin)
            .NotNull()
            .WithMessage("Must specify whether the attachment is for an assignment, submission, or course module.")
            .IsInEnum()
            .WithMessage("The origin is invalid.");
        RuleFor(e => e.Name)
            .NotEmpty()
            .WithMessage("The attachment's name is required.")
            .Length(3, 31)
            .WithMessage("The attachment's name must have between 3 and 31 characters.");
        RuleFor(e => e.Description)
            .MaximumLength(255)
            .WithMessage("The attachment's description must have at most 255 characters.");
        RuleFor(e => e.AssignmentId)
            .NotNull().When(e => e.Origin == AttachmentOrigin.Assignment)
            .WithMessage("Must specify the assignment the attachment is attached to.");
        RuleFor(e => e.SubmissionId)
            .NotNull().When(e => e.Origin == AttachmentOrigin.Submission)
            .WithMessage("Must specify the submission the attachment is attached to.");
        RuleFor(e => e.CourseModuleId)
            .NotNull().When(e => e.Origin == AttachmentOrigin.CourseModule)
            .WithMessage("Must specify the course module the attachment is attached to.");
    }
}