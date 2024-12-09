using chalk.Server.DTOs.Course;

namespace chalk.Server.Services.Interfaces;

public interface ICourseService
{
    public Task<IEnumerable<CourseResponseDTO>> GetCoursesAsync();

    public Task<CourseResponseDTO> GetCourseAsync(long courseId);

    public Task<CourseResponseDTO> CreateCourseAsync(CreateCourseDTO createCourseDto);

    public Task<CourseResponseDTO> UpdateCourseAsync(long courseId, UpdateCourseDTO updateCourseDTO);

    public Task DeleteCourseAsync(long courseId);
}