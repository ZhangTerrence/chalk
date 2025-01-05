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
        builder.Property(e => e.Open).IsRequired();
        builder.Property(e => e.DueDate);
        builder.Property(e => e.AllowedAttempts);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.AssignmentGroup)
            .WithMany(e => e.Assignments)
            .HasForeignKey(e => e.AssignmentGroupId)
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