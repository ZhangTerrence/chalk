using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using File = chalk.Server.Entities.File;

namespace chalk.Server.Data.Configurations;

public class FileConfiguration : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.ToTable("files");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.FileUrl).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();
    }
}