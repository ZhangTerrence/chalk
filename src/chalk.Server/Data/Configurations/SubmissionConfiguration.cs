using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> builder)
    {
        builder.ToTable("submissions");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Grade);
        builder.Property(e => e.Feedback).HasMaxLength(1023);
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Assignment)
            .WithMany(e => e.Submissions)
            .HasForeignKey(e => e.AssignmentId)
            .IsRequired();
        builder
            .HasMany(e => e.Attachments)
            .WithOne(e => e.Submission)
            .HasForeignKey(e => e.SubmissionId);
    }
}