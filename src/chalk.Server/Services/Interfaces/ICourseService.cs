using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseResponse>> GetCoursesAsync();

    public Task<CourseResponse> GetCourseAsync(long courseId);

    public Task<CourseResponse> CreateCourseAsync(string userId, CreateCourseRequest request);

    public Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest request);

    public Task DeleteCourseAsync(long courseId);
}