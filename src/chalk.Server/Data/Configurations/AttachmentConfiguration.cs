using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("attachments");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Uri).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Assignment)
            .WithMany(e => e.Attachments)
            .HasForeignKey(e => e.AssignmentId);
        builder
            .HasOne(e => e.Submission)
            .WithMany(e => e.Attachments)
            .HasForeignKey(e => e.SubmissionId);
        builder
            .HasOne(e => e.CourseModule)
            .WithMany(e => e.Attachments)
            .HasForeignKey(e => e.CourseModuleId);
    }
}