using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class ChannelRolePermissionConfiguration : IEntityTypeConfiguration<ChannelRolePermission>
{
  public void Configure(EntityTypeBuilder<ChannelRolePermission> builder)
  {
    builder.ToTable("channel_role_permissions");

    builder.HasKey(e => new { e.ChannelId, e.RoleId });

    builder.Property(e => e.Permissions).IsRequired();
  }
}
