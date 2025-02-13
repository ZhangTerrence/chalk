using Server.Common.Requests.Course;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

/// <summary>
/// Interface for course services.
/// </summary>
public interface ICourseService
{
  /// <summary>
  /// Gets all courses.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>A list of all the courses.</returns>
  public Task<IEnumerable<Course>> GetCoursesAsync(long requesterId);

  /// <summary>
  /// Gets a course by id.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <returns>The course</returns>
  public Task<Course> GetCourseByIdAsync(long courseId, long requesterId);

  /// <summary>
  /// Creates a course.
  /// </summary>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="CreateRequest" /> for more details.</param>
  /// <returns>The created course.</returns>
  public Task<Course> CreateCourseAsync(long requesterId, CreateRequest request);

  /// <summary>
  /// Updates a course.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  /// <param name="request">The request body. See <see cref="UpdateRequest" /> for more details.</param>
  /// <returns>The updated course.</returns>
  public Task<Course> UpdateCourseAsync(long courseId, long requesterId, UpdateRequest request);

  /// <summary>
  /// Deletes a course.
  /// </summary>
  /// <param name="courseId">The course's id.</param>
  /// <param name="requesterId">The requester's id.</param>
  public Task DeleteCourseAsync(long courseId, long requesterId);
}
