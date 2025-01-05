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

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.PreviewImage);
        builder.Property(e => e.Code).HasMaxLength(31);
        builder.Property(e => e.Public).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Organization)
            .WithMany(e => e.Courses)
            .HasForeignKey(e => e.OrganizationId);
        builder
            .HasMany(e => e.Users)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Roles)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Modules)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.AssignmentGroups)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Channels)
            .WithOne(e => e.Course)
            .HasForeignKey(e => e.CourseId);
        builder
            .HasMany(e => e.Tags)
            .WithOne()
            .HasForeignKey(e => e.CourseId);
    }
}