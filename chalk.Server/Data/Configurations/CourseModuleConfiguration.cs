using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class CourseModuleConfiguration : IEntityTypeConfiguration<CourseModule>
{
    public void Configure(EntityTypeBuilder<CourseModule> builder)
    {
        builder.ToTable("course_modules");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Name)
            .HasMaxLength(31)
            .IsRequired();
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
            .HasOne(e => e.Course)
            .WithMany(e => e.CourseModules)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Attachments)
            .WithOne(e => e.CourseModule);
    }
}