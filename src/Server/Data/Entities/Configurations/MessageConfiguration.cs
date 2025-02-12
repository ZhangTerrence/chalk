using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
  public void Configure(EntityTypeBuilder<Message> builder)
  {
    builder.ToTable("messages");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Text).HasMaxLength(1023).IsRequired();
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();
  }
}