using chalk.Server.DTOs.Course;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseDTO>> GetCoursesAsync();

    public Task<CourseDTO> GetCourseAsync(long courseId);

    public Task<CourseDTO> CreateCourseAsync(CreateCourseDTO createCourseDto);

    public Task<CourseDTO> UpdateCourseAsync(long courseId, UpdateCourseDTO updateCourseDTO);

    public Task DeleteCourseAsync(long courseId);
}