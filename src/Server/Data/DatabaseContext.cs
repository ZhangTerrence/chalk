using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Data.Entities;
using File = Server.Data.Entities.File;
using Module = Server.Data.Entities.Module;

namespace Server.Data;

internal class DatabaseContext : IdentityDbContext<User, IdentityRole<long>, long>
{
  public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
  {
  }

  public DbSet<IdentityUserRole<long>> UserIdentityRoles { get; set; }
  public DbSet<IdentityRole<long>> IdentityRoles { get; set; }
  public DbSet<IdentityRoleClaim<long>> IdentityRoleClaims { get; set; }
  public DbSet<Assignment> Assignments { get; set; }
  public DbSet<AssignmentGroup> AssignmentGroups { get; set; }
  public DbSet<Channel> Channels { get; set; }
  public DbSet<ChannelRolePermission> ChannelRolePermissions { get; set; }
  public DbSet<Course> Courses { get; set; }
  public DbSet<File> Files { get; set; }
  public DbSet<Message> Messages { get; set; }
  public DbSet<Module> Modules { get; set; }
  public DbSet<Organization> Organizations { get; set; }
  public new DbSet<Role> Roles { get; set; }
  public DbSet<Submission> Submissions { get; set; }
  public DbSet<Tag> Tags { get; set; }
  public DbSet<UserChannel> UserChannels { get; set; }
  public DbSet<UserCourse> UserCourses { get; set; }
  public DbSet<UserOrganization> UserOrganizations { get; set; }
  public new DbSet<UserRole> UserRoles { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<User>(e => e.ToTable("users"));
    builder.Entity<IdentityRole<long>>(e => e.ToTable("identity_roles"));
    builder.Entity<IdentityUserClaim<long>>(e => e.ToTable("user_claims"));
    builder.Entity<IdentityUserToken<long>>(e => e.ToTable("user_tokens"));
    builder.Entity<IdentityUserLogin<long>>(e => e.ToTable("user_logins"));
    builder.Entity<IdentityRoleClaim<long>>(e => e.ToTable("identity_roles_claims"));
    builder.Entity<IdentityUserRole<long>>(e => e.ToTable("user_identity_roles"));

    builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    builder.Entity<IdentityRole<long>>().HasData(
      new IdentityRole<long>
      {
        Id = 1,
        Name = "Admin",
        NormalizedName = "ADMIN"
      },
      new IdentityRole<long>
      {
        Id = 2,
        Name = "User",
        NormalizedName = "USER"
      }
    );
  }
}
