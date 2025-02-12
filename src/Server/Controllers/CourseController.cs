using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Common.Interfaces;
using Server.Common.Mappings;
using Server.Common.Requests.Course;
using Server.Common.Responses;
using Server.Common.Utilities;
using Server.Infrastructure.Filters;

namespace Server.Controllers;

[ApiController]
[Route("/api/courses")]
[Authorize]
public class CourseController : ControllerBase
{
  private readonly ICourseService _courseService;

  public CourseController(ICourseService courseService)
  {
    this._courseService = courseService;
  }

  [HttpGet]
  public async Task<IActionResult> GetCourses()
  {
    var courses = await this._courseService.GetCoursesAsync(this.User.GetUserId());
    return this.Ok(new Response<IEnumerable<CourseResponse>>(null, courses.Select(e => e.ToResponse())));
  }

  [HttpGet("{courseId:long}")]
  public async Task<IActionResult> GetCourse([FromRoute] long courseId)
  {
    var course = await this._courseService.GetCourseByIdAsync(courseId, this.User.GetUserId());
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }

  [HttpPost]
  public async Task<IActionResult> CreateCourse([FromBody] [Validate] CreateRequest request)
  {
    var course = await this._courseService.CreateCourseAsync(this.User.GetUserId(), request);
    return this.Created(nameof(this.CreateCourse), new Response<CourseResponse>(null, course.ToResponse()));
  }


  [HttpPut("{courseId:long}")]
  public async Task<IActionResult> UpdateCourse([FromRoute] long courseId, [FromForm] [Validate] UpdateRequest request)
  {
    var course = await this._courseService.UpdateCourseAsync(courseId, this.User.GetUserId(), request);
    return this.Ok(new Response<CourseResponse>(null, course.ToResponse()));
  }


  [HttpDelete("{courseId:long}")]
  public async Task<IActionResult> DeleteCourse([FromRoute] long courseId)
  {
    await this._courseService.DeleteCourseAsync(courseId, this.User.GetUserId());
    return this.NoContent();
  }
}
