using chalk.Server.Shared;

namespace chalk.Server.DTOs.Requests;

/// <summary>
/// A request to create an attachment for either an assignment, submission, or course module.
/// </summary>
/// <param name="Origin">Whether it is for an assignment, submission, or course module.</param>
/// <param name="Name">The attachment's name.</param>
/// <param name="Description">The attachment's description.</param>
/// <param name="Resource">The attachment's resource.</param>
/// <param name="AssignmentId">The id of the assignment the attachment is attached to.</param>
/// <param name="SubmissionId">The id of the submission the attachment is attached to.</param>
/// <param name="CourseModuleId">The id of the course module the attachment is attached to.</param>
/// <remarks>
///     <paramref name="Origin"/>, <paramref name="Name"/>, and <paramref name="Resource"/> are required.
///     <paramref name="AssignmentId"/>, <paramref name="SubmissionId"/>, and <paramref name="CourseModuleId"/> are conditionally required based on <paramref name="Origin"/>.
/// </remarks>
public record CreateAttachmentRequest(
    AttachmentOrigin? Origin,
    string Name,
    string? Description,
    IFormFile Resource,
    long? AssignmentId,
    long? SubmissionId,
    long? CourseModuleId
);