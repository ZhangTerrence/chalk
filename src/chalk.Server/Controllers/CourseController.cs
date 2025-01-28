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
    private readonly IValidator<CreateCourseModuleRequest> _createCourseModuleRequestValidator;
    private readonly IValidator<UpdateCourseModuleRequest> _updateCourseModuleRequestValidator;
    private readonly IValidator<CreateAttachmentRequest> _createAttachmentRequestValidator;

    public CourseController(
        ICourseService courseService,
        IValidator<CreateCourseRequest> createCourseRequestValidator,
        IValidator<UpdateCourseRequest> updateCourseRequestValidator,
        IValidator<CreateCourseModuleRequest> createCourseModuleRequestValidator,
        IValidator<UpdateCourseModuleRequest> updateCourseModuleRequestValidator,
        IValidator<CreateAttachmentRequest> createAttachmentRequestValidator
    )
    {
        _courseService = courseService;
        _createCourseRequestValidator = createCourseRequestValidator;
        _updateCourseRequestValidator = updateCourseRequestValidator;
        _createCourseModuleRequestValidator = createCourseModuleRequestValidator;
        _updateCourseModuleRequestValidator = updateCourseModuleRequestValidator;
        _createAttachmentRequestValidator = createAttachmentRequestValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _courseService.GetCoursesAsync();
        return Ok(new Response<IEnumerable<CourseResponse>>(null, courses.Select(e => e.ToResponse())));
    }

    [HttpGet("{courseId:long}")]
    public async Task<IActionResult> GetCourse([FromRoute] long courseId)
    {
        var course = await _courseService.GetCourseAsync(courseId);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var validationResult = await _createCourseRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.CreateCourseAsync(User.GetUserId(), request);
        return Created(nameof(CreateCourse), new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPut("{courseId:long}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] long courseId, [FromBody] UpdateCourseRequest request)
    {
        var validationResult = await _updateCourseRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.UpdateCourseAsync(courseId, request);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpDelete("{courseId:long}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
    {
        await _courseService.DeleteCourseAsync(courseId);
        return NoContent();
    }

    [HttpPost("modules")]
    public async Task<IActionResult> AddCourseModule([FromBody] CreateCourseModuleRequest request)
    {
        var validationResult = await _createCourseModuleRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.CreateCourseModuleAsync(request);
        return Created(nameof(AddCourseModule), new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPut("modules/{courseModuleId:long}")]
    public async Task<IActionResult> UpdateCourseModule([FromRoute] long courseModuleId, [FromBody] UpdateCourseModuleRequest request)
    {
        var validationResult = await _updateCourseModuleRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.UpdateCourseModuleAsync(courseModuleId, request);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpDelete("modules/{courseModuleId:long}")]
    public async Task<IActionResult> DeleteCourseModule([FromRoute] long courseModuleId)
    {
        var course = await _courseService.DeleteCourseModuleAsync(courseModuleId);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPost("modules/{courseModuleId:long}/attach")]
    public async Task<IActionResult> AddCourseModuleAttachment([FromRoute] long courseModuleId, [FromBody] CreateAttachmentRequest request)
    {
        var validationResult = await _createAttachmentRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.AddCourseModuleAttachmentAsync(courseModuleId, request);
        return Created(nameof(AddCourseModuleAttachment), new Response<CourseResponse>(null, course.ToResponse()));
    }
}