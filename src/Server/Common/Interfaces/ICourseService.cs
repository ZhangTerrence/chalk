using Server.Common.Requests.Course;
using Server.Data.Entities;

namespace Server.Common.Interfaces;

public interface ICourseService
{
  public Task<IEnumerable<Course>> GetCoursesAsync(long requesterId);

  public Task<Course> GetCourseByIdAsync(long courseId, long requesterId);

  public Task<Course> CreateCourseAsync(long requesterId, CreateRequest request);

  public Task<Course> UpdateCourseAsync(long courseId, long requesterId, UpdateRequest request);

  public Task DeleteCourseAsync(long courseId, long requesterId);
}
