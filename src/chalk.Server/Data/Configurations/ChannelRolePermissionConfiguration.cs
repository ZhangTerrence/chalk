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

        builder.Property(e => e.Permissions).IsRequired();

        builder
            .HasOne(e => e.Channel)
            .WithMany(e => e.RolePermissions)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasOne(e => e.CourseRole)
            .WithMany(e => e.ChannelPermissions)
            .HasForeignKey(e => e.CourseRoleId);
        builder
            .HasOne(e => e.OrganizationRole)
            .WithMany(e => e.ChannelPermissions)
            .HasForeignKey(e => e.OrganizationRoleId);
        builder
            .HasMany(e => e.Users)
            .WithOne(e => e.RolePermission)
            .HasForeignKey(e => new { e.ChannelId, e.CourseRoleId });
    }
}