using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
  public void Configure(EntityTypeBuilder<Submission> builder)
  {
    builder.ToTable("submissions");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Grade);
    builder.Property(e => e.Feedback).HasMaxLength(1023);
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

    builder
      .HasMany(e => e.Files)
      .WithOne()
      .HasForeignKey(e => e.SubmissionId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}