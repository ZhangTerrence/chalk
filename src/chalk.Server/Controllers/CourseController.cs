using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/courses"), Authorize]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    private readonly IValidator<CreateCourseRequest> _createCourseRequestValidator;
    private readonly IValidator<UpdateCourseRequest> _updateCourseRequestValidator;

    public CourseController(
        ICourseService courseService,
        IValidator<CreateCourseRequest> createCourseRequestValidator,
        IValidator<UpdateCourseRequest> updateCourseRequestValidator
    )
    {
        _courseService = courseService;
        _createCourseRequestValidator = createCourseRequestValidator;
        _updateCourseRequestValidator = updateCourseRequestValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetCoursesAsync();
        return Ok(new ApiResponse<IEnumerable<CourseResponse>>(null, courses.Select(e => e.ToResponse())));
    }

    [HttpGet("{courseId:long}")]
    public async Task<IActionResult> GetCourse([FromRoute] long courseId)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        return Ok(new ApiResponse<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var validationResult = await _createCourseRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.CreateCourseAsync(User.GetUserId(), request);
        return Created(nameof(CreateCourse), new ApiResponse<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPut("{courseId:long}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] long courseId, [FromBody] UpdateCourseRequest request)
    {
        var validationResult = await _updateCourseRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new ApiResponse<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.UpdateCourseAsync(courseId, request);
        return Ok(new ApiResponse<CourseResponse>(null, course.ToResponse()));
    }

    [HttpDelete("{courseId:long}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
    {
        await _courseService.DeleteCourseAsync(courseId);
        return NoContent();
    }
}