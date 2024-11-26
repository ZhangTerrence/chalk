using chalk.Server.DTOs;
using chalk.Server.DTOs.Course;
using chalk.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Authorize(Roles = "User,Admin")]
[Route("/api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetCoursesAsync();
        return Ok(new ApiResponseDTO<IEnumerable<CourseDTO>>(courses));
    }

    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetCourse([FromRoute] Guid courseId)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        return Ok(new ApiResponseDTO<CourseDTO>(course));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
    {
        var createdCourse = await _courseService.CreateCourseAsync(createCourseDTO);
        return Ok(new ApiResponseDTO<CourseDTO>(createdCourse));
    }

    [HttpPatch("{courseId:guid}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] Guid courseId, [FromBody] UpdateCourseDTO updateCourseDTO)
    {
        var updatedCourse = await _courseService.UpdateCourseAsync(courseId, updateCourseDTO);
        return Ok(new ApiResponseDTO<CourseDTO>(updatedCourse));
    }

    [HttpDelete("{courseId:guid}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId)
    {
        await _courseService.DeleteCourseAsync(courseId);
        return NoContent();
    }
}