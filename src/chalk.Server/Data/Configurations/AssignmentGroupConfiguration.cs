using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class AssignmentGroupConfiguration : IEntityTypeConfiguration<AssignmentGroup>
{
    public void Configure(EntityTypeBuilder<AssignmentGroup> builder)
    {
        builder.ToTable("assignment_groups");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Weight).IsRequired();

        builder
            .HasOne(e => e.Course)
            .WithMany(c => c.AssignmentGroups)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Assignments)
            .WithOne(e => e.AssignmentGroup)
            .HasForeignKey(e => e.AssignmentGroupId)
            .IsRequired();
    }
}