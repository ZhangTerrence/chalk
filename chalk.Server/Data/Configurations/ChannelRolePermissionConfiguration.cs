using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class ChannelRolePermissionConfiguration : IEntityTypeConfiguration<ChannelRolePermission>
{
    public void Configure(EntityTypeBuilder<ChannelRolePermission> builder)
    {
        builder.ToTable("channel_role_permissions");

        builder.HasKey(e => new { e.ChannelId, e.CourseRoleId });

        builder.Property(e => e.Permissions)
            .HasDefaultValue(0L)
            .IsRequired();

        builder
            .HasOne(e => e.Channel)
            .WithMany(e => e.ChannelRolePermissions)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasOne(e => e.CourseRole)
            .WithMany(e => e.ChannelRolePermissions)
            .HasForeignKey(e => e.CourseRoleId)
            .IsRequired();
        builder
            .HasMany(e => e.ChannelParticipants)
            .WithOne(e => e.ChannelRolePermission)
            .HasForeignKey(e => new { e.ChannelId, e.CourseRoleId });
    }
}