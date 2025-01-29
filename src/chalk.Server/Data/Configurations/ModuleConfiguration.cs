using chalk.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace chalk.Server.Data.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        builder.ToTable("modules");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasMaxLength(31).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(255);
        builder.Property(e => e.RelativeOrder).IsRequired();
        builder.Property(e => e.CreatedDate).IsRequired();
        builder.Property(e => e.UpdatedDate).IsRequired();

        builder
            .HasMany(e => e.Files)
            .WithOne()
            .HasForeignKey(e => e.CourseModuleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}