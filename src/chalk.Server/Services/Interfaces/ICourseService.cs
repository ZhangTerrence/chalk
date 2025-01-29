using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<Course>> GetCoursesAsync();

    public Task<Course> GetCourseAsync(long courseId);

    public Task<Module> GetModuleAsync(long moduleId);

    public Task<Course> CreateCourseAsync(long userId, CreateCourseRequest request);

    public Task<Module> CreateCourseModuleAsync(long courseId, CreateModuleRequest request);

    public Task<Course> UpdateCourseAsync(long courseId, UpdateCourseRequest request);

    public Task<Module> UpdateCourseModuleAsync(long courseId, long moduleId, UpdateModuleRequest request);

    public Task DeleteCourseAsync(long courseId);

    public Task DeleteCourseModuleAsync(long courseId, long moduleId);
}