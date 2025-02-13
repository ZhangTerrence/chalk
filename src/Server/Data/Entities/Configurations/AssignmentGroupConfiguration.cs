using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class AssignmentGroupConfiguration : IEntityTypeConfiguration<AssignmentGroup>
{
  public void Configure(EntityTypeBuilder<AssignmentGroup> builder)
  {
    builder.ToTable("assignment_groups");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.Weight).IsRequired();
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

    builder
      .HasMany(e => e.Assignments)
      .WithOne()
      .HasForeignKey(e => e.AssignmentGroupId)
      .IsRequired();
  }
}
