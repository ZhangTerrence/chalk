using System.Globalization;
using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Mappings;

public static class AssignmentMappings
{
    public static AssignmentGroup ToEntity(this CreateAssignmentGroupRequest request, long courseId)
    {
        return new AssignmentGroup
        {
            Name = request.Name,
            Description = request.Description,
            Weight = request.Weight!.Value,
            CourseId = courseId
        };
    }

    public static AssignmentGroupDTO ToDTO(this AssignmentGroup assignmentGroup)
    {
        return new AssignmentGroupDTO(
            assignmentGroup.Id,
            assignmentGroup.Name,
            assignmentGroup.Description,
            assignmentGroup.Weight,
            assignmentGroup.Assignments.Select(e => e.ToDTO())
        );
    }

    public static Assignment ToEntity(this CreateAssignmentRequest request, long assignmentGroupId)
    {
        return new Assignment
        {
            Name = request.Name,
            Description = request.Description,
            IsOpen = request.IsOpen!.Value,
            DueDate = request.DueDate,
            AllowedAttempts = request.AllowedAttempts,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            AssignmentGroupId = assignmentGroupId
        };
    }

    public static AssignmentDTO ToDTO(this Assignment assignment)
    {
        return new AssignmentDTO(
            assignment.Id,
            assignment.Name,
            assignment.Description,
            assignment.IsOpen,
            assignment.DueDate?.ToString(CultureInfo.CurrentCulture),
            assignment.AllowedAttempts,
            assignment.CreatedDate.ToString(CultureInfo.CurrentCulture),
            assignment.UpdatedDate.ToString(CultureInfo.CurrentCulture)
        );
    }
}