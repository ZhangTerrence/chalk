using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<Course>> GetCoursesAsync();

    public Task<Course> GetCourseAsync(long courseId);

    public Task<Course> CreateCourseAsync(long userId, CreateCourseRequest request);

    public Task<Course> UpdateCourseAsync(long courseId, UpdateCourseRequest request);

    public Task DeleteCourseAsync(long courseId);
}