using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
{
    public void Configure(EntityTypeBuilder<UserCourse> builder)
    {
        builder.ToTable("user_courses");

        builder.HasKey(e => new { e.UserId, e.CourseId });

        builder.Property(e => e.UserStatus).IsRequired();
        builder.Property(e => e.Grade).IsRequired();
        builder.Property(e => e.JoinedDate);

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
            .HasOne(e => e.CourseRole)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
    }
}