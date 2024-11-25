using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Text)
            .HasMaxLength(1023)
            .IsRequired();
        builder.Property(e => e.CreatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();
        builder.Property(e => e.UpdatedDate)
            .HasDefaultValue(DateTime.UtcNow)
            .IsRequired();

        builder
            .HasOne(e => e.Channel)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => e.ChannelId)
            .IsRequired();
        builder
            .HasOne(e => e.ChannelParticipant)
            .WithMany(e => e.Messages)
            .HasForeignKey(e => new { e.ChannelId, e.UserId })
            .IsRequired();
    }
}