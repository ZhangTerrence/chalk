using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Course;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

/// <summary>
/// Routes for managing courses.
/// </summary>
[ApiController] [Authorize]
[Route("/api/courses")]
[Produces("application/json")]
public class CourseController : ControllerBase
{
  private readonly ICourseService _courseService;

  internal CourseController(ICourseService courseService)
  {
    this._courseService = courseService;
  }

  /// <summary>
  /// Gets all courses.
  /// </summary>
  /// <returns>A list of all the courses.</returns>
  [HttpGet]
  [ProducesResponseType<Response<IEnumerable<CourseResponse>>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCourses()
  {
    var courses = await this._courseService.GetCoursesAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<CourseResponse>>(null, courses.Select(e => e.ToResponse())));
  }

  /// <summary>
  /// Gets a course.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <returns>The course.</returns>
  [HttpGet("{courseId:long}")]
  [ProducesResponseType<Response<CourseResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> GetCourse([FromRoute] long courseId)
  {
    var course = await this._courseService.GetCourseByIdAsync(courseId, this.User.GetUserId());
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }

  /// <summary>
  /// Creates a course.
  /// </summary>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created course.</returns>
  [HttpPost]
  [ProducesResponseType<Response<CourseResponse>>(StatusCodes.Status201Created)]
  public async Task<IActionResult> CreateCourse([FromBody] [Validate] CreateRequest request)
  {
    var course = await this._courseService.CreateCourseAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateCourse), new Response<CourseResponse>(null, course.ToResponse()));
  }

  /// <summary>
  /// Updates a course.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated course.</returns>
  [HttpPut("{courseId:long}")]
  [ProducesResponseType<Response<CourseResponse>>(StatusCodes.Status200OK)]
  public async Task<IActionResult> UpdateCourse([FromRoute] long courseId, [FromForm] [Validate] UpdateRequest request)
  {
    var course = await this._courseService.UpdateCourseAsync(courseId, this.User.GetUserId(), request);
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }


  /// <summary>
  /// Deletes a course.
  /// </summary>
  /// <param name="courseId"></param>
  /// <returns></returns>
  [HttpDelete("{courseId:long}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
  {
    await this._courseService.DeleteCourseAsync(courseId, this.User.GetUserId());
    return this.NoContent();
  }
}
