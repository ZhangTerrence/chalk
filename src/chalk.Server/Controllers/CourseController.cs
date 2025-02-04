using chalk.Server.DTOs;
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
    private readonly IValidator<CreateModuleRequest> _createModuleRequestValidator;
    private readonly IValidator<CreateAssignmentGroupRequest> _createAssignmentGroupRequestValidator;
    private readonly IValidator<CreateAssignmentRequest> _createAssignmentRequestValidator;
    private readonly IValidator<UpdateCourseRequest> _updateCourseRequestValidator;
    private readonly IValidator<ReorderModulesRequest> _reorderModulesRequestValidator;
    private readonly IValidator<UpdateModuleRequest> _updateModuleRequestValidator;
    private readonly IValidator<UpdateAssignmentGroupRequest> _updateAssignmentGroupRequestValidator;
    private readonly IValidator<UpdateAssignmentRequest> _updateAssignmentRequestValidator;

    public CourseController(
        ICourseService courseService,
        IValidator<CreateCourseRequest> createCourseRequestValidator,
        IValidator<CreateModuleRequest> createModuleRequestValidator,
        IValidator<CreateAssignmentGroupRequest> createAssignmentGroupRequestValidator,
        IValidator<CreateAssignmentRequest> createAssignmentRequestValidator,
        IValidator<UpdateCourseRequest> updateCourseRequestValidator,
        IValidator<ReorderModulesRequest> reorderModulesRequestValidator,
        IValidator<UpdateModuleRequest> updateModuleRequestValidator,
        IValidator<UpdateAssignmentGroupRequest> updateAssignmentGroupRequestValidator,
        IValidator<UpdateAssignmentRequest> updateAssignmentRequestValidator
    )
    {
        _courseService = courseService;
        _createCourseRequestValidator = createCourseRequestValidator;
        _createModuleRequestValidator = createModuleRequestValidator;
        _createAssignmentGroupRequestValidator = createAssignmentGroupRequestValidator;
        _createAssignmentRequestValidator = createAssignmentRequestValidator;
        _updateCourseRequestValidator = updateCourseRequestValidator;
        _reorderModulesRequestValidator = reorderModulesRequestValidator;
        _updateModuleRequestValidator = updateModuleRequestValidator;
        _updateAssignmentGroupRequestValidator = updateAssignmentGroupRequestValidator;
        _updateAssignmentRequestValidator = updateAssignmentRequestValidator;
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

    [HttpPost("{courseId:long}/modules")]
    public async Task<IActionResult> CreateModule([FromRoute] long courseId, [FromBody] CreateModuleRequest request)
    {
        var validationResult = await _createModuleRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var module = await _courseService.CreateModuleAsync(courseId, request);
        return Created(nameof(CreateModule), new Response<ModuleDTO>(null, module.ToDTO()));
    }

    [HttpPost("{courseId:long}/assignment-groups")]
    public async Task<IActionResult> CreateAssignmentGroup(
        [FromRoute] long courseId,
        [FromBody] CreateAssignmentGroupRequest request
    )
    {
        var validationResult = await _createAssignmentGroupRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var assignmentGroup = await _courseService.CreateAssignmentGroupAsync(courseId, request);
        return Created(nameof(CreateAssignmentGroup), new Response<AssignmentGroupDTO>(null, assignmentGroup.ToDTO()));
    }

    [HttpPost("{courseId:long}/assignment-groups/{assignmentGroupId:long}")]
    public async Task<IActionResult> CreateAssignment(
        [FromRoute] long courseId,
        [FromRoute] long assignmentGroupId,
        [FromBody] CreateAssignmentRequest request
    )
    {
        var validationResult = await _createAssignmentRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var assignment = await _courseService.CreateAssignmentAsync(courseId, assignmentGroupId, request);
        return Created(nameof(CreateAssignment), new Response<AssignmentDTO>(null, assignment.ToDTO()));
    }

    [HttpPut("{courseId:long}")]
    public async Task<IActionResult> UpdateCourse([FromRoute] long courseId, [FromForm] UpdateCourseRequest request)
    {
        var validationResult = await _updateCourseRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.UpdateCourseAsync(courseId, request);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPut("{courseId:long}/modules")]
    public async Task<IActionResult> ReorderModules([FromRoute] long courseId, [FromBody] ReorderModulesRequest request)
    {
        var validationResult = await _reorderModulesRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var course = await _courseService.ReorderModulesAsync(courseId, request);
        return Ok(new Response<CourseResponse>(null, course.ToResponse()));
    }

    [HttpPut("{courseId:long}/modules/{moduleId:long}")]
    public async Task<IActionResult> UpdateCourseModule(
        [FromRoute] long courseId,
        [FromRoute] long moduleId,
        [FromBody] UpdateModuleRequest request
    )
    {
        var validationResult = await _updateModuleRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var module = await _courseService.UpdateModuleAsync(courseId, moduleId, request);
        return Ok(new Response<ModuleDTO>(null, module.ToDTO()));
    }

    [HttpPut("{courseId:long}/assignment-groups/{assignmentGroupId:long}")]
    public async Task<IActionResult> UpdateAssignmentGroup(
        [FromRoute] long courseId,
        [FromRoute] long assignmentGroupId,
        [FromBody] UpdateAssignmentGroupRequest request
    )
    {
        var validationResult = await _updateAssignmentGroupRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var assignmentGroup = await _courseService.UpdateAssignmentGroupAsync(courseId, assignmentGroupId, request);
        return Ok(new Response<AssignmentGroupDTO>(null, assignmentGroup.ToDTO()));
    }

    [HttpPut("{courseId:long}/assignment-groups/{assignmentGroupId:long}/{assignmentId:long}")]
    public async Task<IActionResult> UpdateAssignment(
        [FromRoute] long courseId,
        [FromRoute] long assignmentGroupId,
        [FromRoute] long assignmentId,
        [FromBody] UpdateAssignmentRequest request
    )
    {
        var validationResult = await _updateAssignmentRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var assignment = await _courseService.UpdateAssignmentAsync(courseId, assignmentGroupId, assignmentId, request);
        return Ok(new Response<AssignmentDTO>(null, assignment.ToDTO()));
    }

    [HttpDelete("{courseId:long}")]
    public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
    {
        await _courseService.DeleteCourseAsync(courseId);
        return NoContent();
    }

    [HttpDelete("{courseId:long}/modules/{moduleId:long}")]
    public async Task<IActionResult> DeleteCourseModule([FromRoute] long courseId, [FromRoute] long moduleId)
    {
        await _courseService.DeleteModuleAsync(courseId, moduleId);
        return NoContent();
    }

    [HttpDelete("{courseId:long}/assignment-groups/{assignmentGroupId:long}")]
    public async Task<IActionResult> DeleteAssignmentGroup([FromRoute] long courseId, [FromRoute] long assignmentGroupId)
    {
        await _courseService.DeleteAssignmentGroupAsync(courseId, assignmentGroupId);
        return NoContent();
    }

    [HttpDelete("{courseId:long}/assignment-groups/{assignmentGroupId:long}/{assignmentId:long}")]
    public async Task<IActionResult> DeleteAssignment([FromRoute] long courseId, long assignmentGroupId, long assignmentId)
    {
        await _courseService.DeleteAssignmentAsync(courseId, assignmentGroupId, assignmentId);
        return NoContent();
    }
}