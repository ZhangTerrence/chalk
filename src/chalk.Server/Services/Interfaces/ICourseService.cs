using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseResponse>> GetCoursesAsync();

    public Task<CourseResponse> GetCourseAsync(long courseId);

    public Task<CourseResponse> CreateCourseAsync(CreateCourseRequest createCourseRequest);

    public Task<CourseResponse> UpdateCourseAsync(long courseId, UpdateCourseRequest updateCourseRequest);

    public Task DeleteCourseAsync(long courseId);
}