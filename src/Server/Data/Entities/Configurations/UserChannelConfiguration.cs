using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class UserChannelConfiguration : IEntityTypeConfiguration<UserChannel>
{
  public void Configure(EntityTypeBuilder<UserChannel> builder)
  {
    builder.ToTable("user_channels");

    builder.HasKey(e => new { e.UserId, e.ChannelId });

    builder
      .HasOne(e => e.User)
      .WithMany(e => e.DirectMessages)
      .HasForeignKey(e => e.UserId)
      .IsRequired();
    builder
      .HasOne(e => e.Channel)
      .WithMany(e => e.Users)
      .HasForeignKey(e => e.ChannelId)
      .IsRequired();
    builder
      .HasMany(e => e.Messages)
      .WithOne()
      .HasForeignKey(e => new { e.UserId, e.ChannelId })
      .IsRequired();
  }
}
