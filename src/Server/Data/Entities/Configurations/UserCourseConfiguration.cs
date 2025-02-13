using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
{
  public void Configure(EntityTypeBuilder<UserCourse> builder)
  {
    builder.ToTable("user_courses");

    builder.HasKey(e => new { e.UserId, e.CourseId });

    builder.Property(e => e.Status).IsRequired();
    builder.Property(e => e.Grade);
    builder.Property(e => e.JoinedOnUtc);

    builder
      .HasOne(e => e.User)
      .WithMany(e => e.Courses)
      .HasForeignKey(e => e.UserId)
      .IsRequired();
    builder
      .HasOne(e => e.Course)
      .WithMany(e => e.Users)
      .HasForeignKey(e => e.CourseId)
      .IsRequired();
    builder
      .HasMany(e => e.Roles)
      .WithOne()
      .HasForeignKey(e => new { e.UserId, CourseId = (long)e.CourseId! })
      .OnDelete(DeleteBehavior.Cascade);
  }
}
