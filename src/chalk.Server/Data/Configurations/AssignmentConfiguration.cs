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

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.IsOpen).IsRequired();
        builder.Property(e => e.DueDate);
        builder.Property(e => e.AllowedAttempts);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasMany(e => e.Files)
            .WithOne()
            .HasForeignKey(e => e.AssignmentId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(e => e.Submissions)
            .WithOne()
            .HasForeignKey(e => e.AssignmentId)
            .IsRequired();
    }
}