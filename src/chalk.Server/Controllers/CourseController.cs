using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Services.Interfaces;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/course")]
[Authorize]
public class CourseController : ControllerBase
{
    // Services
    private readonly ICourseService _courseService;

    // Validators
    private readonly IValidator<CreateCourseRequest> _createCourseValidator;
    private readonly IValidator<UpdateCourseRequest> _updateCourseValidator;

    public CourseController(
        ICourseService courseService,
        IValidator<CreateCourseRequest> createCourseValidator,
        IValidator<UpdateCourseRequest> updateCourseValidator
    )
    {
        _courseService = courseService;

        _createCourseValidator = createCourseValidator;
        _updateCourseValidator = updateCourseValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetCoursesAsync();
        return Ok(new ApiResponse<IEnumerable<CourseResponse>>(null, courses));
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetCourse([FromRoute] long id)
    {
        var course = await _courseService.GetCourseAsync(id);
        return Ok(new ApiResponse<CourseResponse>(null, course));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var result = await _createCourseValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var createdCourse = await _courseService.CreateCourseAsync(request);
        return Ok(new ApiResponse<CourseResponse>(null, createdCourse));
    }

    [HttpPatch("{id:long}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] long id, [FromBody] UpdateCourseRequest request)
    {
        var result = await _updateCourseValidator.ValidateAsync(request);
        if (!result.IsValid)
        {
            return BadRequest(new ApiResponse<object>(result.GetErrorMessages()));
        }

        var updatedCourse = await _courseService.UpdateCourseAsync(id, request);
        return Ok(new ApiResponse<CourseResponse>(null, updatedCourse));
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] long id)
    {
        await _courseService.DeleteCourseAsync(id);
        return NoContent();
    }
}