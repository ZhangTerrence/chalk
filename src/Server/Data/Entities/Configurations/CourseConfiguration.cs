using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.ToTable("courses");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
    builder.Property(e => e.Code).HasMaxLength(31);
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.ImageUrl);
    builder.Property(e => e.IsPublic).IsRequired();
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

    builder
      .HasMany(e => e.Users)
      .WithOne(e => e.Course)
      .HasForeignKey(e => e.CourseId)
      .IsRequired();
    builder
      .HasMany(e => e.Roles)
      .WithOne()
      .HasForeignKey(e => e.CourseId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasMany(e => e.Modules)
      .WithOne()
      .HasForeignKey(e => e.CourseId)
      .IsRequired();
    builder
      .HasMany(e => e.AssignmentGroups)
      .WithOne()
      .HasForeignKey(e => e.CourseId)
      .IsRequired();
    builder
      .HasMany(e => e.Channels)
      .WithOne()
      .HasForeignKey(e => e.CourseId)
      .OnDelete(DeleteBehavior.Cascade);
    builder
      .HasMany(e => e.Tags)
      .WithOne()
      .HasForeignKey(e => e.CourseId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
