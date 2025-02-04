using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<Course>> GetCoursesAsync();

    public Task<Course> GetCourseAsync(long courseId);

    public Task<Module> GetModuleAsync(long moduleId);

    public Task<AssignmentGroup> GetAssignmentGroupAsync(long assignmentGroupId);

    public Task<Assignment> GetAssignmentAsync(long assignmentId);

    public Task<Course> CreateCourseAsync(long userId, CreateCourseRequest request);

    public Task<Module> CreateModuleAsync(long courseId, CreateModuleRequest request);

    public Task<AssignmentGroup> CreateAssignmentGroupAsync(long courseId, CreateAssignmentGroupRequest request);

    public Task<Assignment> CreateAssignmentAsync(long courseId, long assignmentGroupId, CreateAssignmentRequest request);

    public Task<Course> UpdateCourseAsync(long courseId, UpdateCourseRequest request);

    public Task<Course> ReorderModulesAsync(long courseId, ReorderModulesRequest request);

    public Task<Module> UpdateModuleAsync(long courseId, long moduleId, UpdateModuleRequest request);

    public Task<AssignmentGroup> UpdateAssignmentGroupAsync(long courseId, long assignmentGroupId, UpdateAssignmentGroupRequest request);

    public Task<Assignment> UpdateAssignmentAsync(long courseId, long assignmentGroupId, long assignmentId, UpdateAssignmentRequest request);

    public Task DeleteCourseAsync(long courseId);

    public Task DeleteModuleAsync(long courseId, long moduleId);

    public Task DeleteAssignmentGroupAsync(long courseId, long assignmentGroupId);

    public Task DeleteAssignmentAsync(long courseId, long assignmentGroupId, long assignmentId);
}