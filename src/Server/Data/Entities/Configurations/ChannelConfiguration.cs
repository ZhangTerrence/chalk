using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class ChannelConfiguration : IEntityTypeConfiguration<Channel>
{
  public void Configure(EntityTypeBuilder<Channel> builder)
  {
    builder.ToTable("channels");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Name).HasMaxLength(31);
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

    builder
      .HasMany(e => e.Users)
      .WithOne(e => e.Channel)
      .HasForeignKey(e => e.ChannelId)
      .IsRequired();
    builder
      .HasMany(e => e.Messages)
      .WithOne()
      .HasForeignKey(e => e.ChannelId)
      .IsRequired();
    builder
      .HasMany(e => e.RolePermissions)
      .WithOne()
      .HasForeignKey(e => e.ChannelId)
      .IsRequired();
  }
}
