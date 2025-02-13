using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Server.Data.Entities.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(e => e.FirstName).HasMaxLength(31).IsRequired();
    builder.Property(e => e.LastName).HasMaxLength(31).IsRequired();
    builder.Property(e => e.DisplayName).HasMaxLength(31).IsRequired();
    builder.Property(e => e.ImageUrl);
    builder.Property(e => e.Description).HasMaxLength(255);
    builder.Property(e => e.CreatedOnUtc).IsRequired();
    builder.Property(e => e.UpdatedOnUtc).IsRequired();

    builder
      .HasMany(e => e.DirectMessages)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId)
      .IsRequired();
    builder
      .HasMany(e => e.Courses)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId)
      .IsRequired();
    builder
      .HasMany(e => e.Organizations)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId)
      .IsRequired();
  }
}
