using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

public class ChannelRolePermissionConfiguration : IEntityTypeConfiguration<ChannelRolePermission>
{
  public void Configure(EntityTypeBuilder<ChannelRolePermission> builder)
  {
    builder.ToTable("channel_role_permissions");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Permissions).IsRequired();
  }
}