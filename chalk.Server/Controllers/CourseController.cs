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

    [HttpGet("{courseId:long}")]
    public async Task<IActionResult> GetCourse([FromRoute] long courseId)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        return Ok(new ApiResponseDTO<CourseDTO>(course));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO createCourseDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var createdCourse = await _courseService.CreateCourseAsync(createCourseDTO);
        return Ok(new ApiResponseDTO<CourseDTO>(createdCourse));
    }

    [HttpPatch("{courseId:long}")]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] long courseId,
        [FromBody] UpdateCourseDTO updateCourseDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDTO<object>(ModelState));
        }

        var updatedCourse = await _courseService.UpdateCourseAsync(courseId, updateCourseDTO);
        return Ok(new ApiResponseDTO<CourseDTO>(updatedCourse));
    }

    [HttpDelete("{courseId:long}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
    {
        await _courseService.DeleteCourseAsync(courseId);
        return NoContent();
    }
}