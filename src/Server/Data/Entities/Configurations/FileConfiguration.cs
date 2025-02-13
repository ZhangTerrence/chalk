using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class FileConfiguration : IEntityTypeConfiguration<File>
{
  public void Configure(EntityTypeBuilder<File> builder)
  {
    builder.ToTable("files");

    builder.HasKey(e => e.Id);

    builder.Property(e => e.Id).ValueGeneratedOnAdd();
    builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.FileUrl).IsRequired();
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();
  }
}
