using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
    public void Configure(EntityTypeBuilder<Assignment> builder)
    {
        builder.ToTable("assignments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Title)
            .HasMaxLength(31)
            .IsRequired();
        builder.Property(e => e.Description)
            .HasDefaultValue(null)
            .HasMaxLength(255);
        builder.Property(e => e.Open)
            .HasDefaultValue(true)
            .IsRequired();
        builder.Property(e => e.MaxGrade)
            .HasDefaultValue(null);
        builder.Property(e => e.DueDate)
            .HasDefaultValue(null);
        builder.Property(e => e.AllowedAttempts)
            .HasDefaultValue(null);
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasOne(e => e.Course)
            .WithMany(e => e.Assignments)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Attachments)
            .WithOne(e => e.Assignment)
            .HasForeignKey(e => e.AssignmentId);
        builder
            .HasMany(e => e.Submissions)
            .WithOne(e => e.Assignment)
            .HasForeignKey(e => e.AssignmentId)
            .IsRequired();
    }
}