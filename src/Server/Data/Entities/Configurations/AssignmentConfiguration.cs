using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
{
  public void Configure(EntityTypeBuilder<Assignment> builder)
  {
    builder.ToTable("assignments");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();
    builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.DueOnUtc);
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

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