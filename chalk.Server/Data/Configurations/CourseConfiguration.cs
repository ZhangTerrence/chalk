using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.Code)
            .HasDefaultValue(null)
            .HasMaxLength(31);
        builder.Property(e => e.Description)
            .HasDefaultValue(null)
            .HasMaxLength(255);
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasOne(e => e.Organization)
            .WithMany(e => e.Courses)
            .HasForeignKey(e => e.OrganizationId)
            .IsRequired();
        builder
            .HasMany(e => e.UserCourses)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.CourseModules)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Assignments)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.CourseRoles)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Channels)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId);
    }
}