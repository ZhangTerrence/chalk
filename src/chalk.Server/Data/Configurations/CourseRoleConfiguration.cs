using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class CourseRoleConfiguration : IEntityTypeConfiguration<CourseRole>
{
    public void Configure(EntityTypeBuilder<CourseRole> builder)
    {
        builder.ToTable("course_roles");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.Permissions).IsRequired();
        builder.Property(e => e.Rank).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasOne(e => e.Course)
            .WithMany(e => e.Roles)
            .HasForeignKey(e => e.CourseId)
            .IsRequired();
        builder
            .HasMany(e => e.Users)
            .WithOne(e => e.CourseRole)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
        builder
            .HasMany(e => e.Channels)
            .WithOne(e => e.CourseRole)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
        builder
            .HasMany(e => e.ChannelPermissions)
            .WithOne(e => e.CourseRole)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
    }
}