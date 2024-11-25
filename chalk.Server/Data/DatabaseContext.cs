using chalk.Server.Data.Configurations;
using chalk.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Data;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Submission> Submissions { get; set; }
    public DbSet<UserOrganization> UserOrganizations { get; set; }
    public DbSet<OrganizationRole> OrganizationRoles { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<CourseRole> CourseRoles { get; set; }
    public DbSet<ChannelParticipant> ChannelParticipants { get; set; }
    public DbSet<ChannelRolePermission> ChannelRolePermissions { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<CourseModule> CourseModules { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>(e => e.ToTable("users"));
        builder.Entity<IdentityRole<Guid>>(e => e.ToTable("roles"));
        builder.Entity<IdentityUserClaim<Guid>>(e => e.ToTable("user_claims"));
        builder.Entity<IdentityUserToken<Guid>>(e => e.ToTable("user_tokens"));
        builder.Entity<IdentityUserLogin<Guid>>(e => e.ToTable("user_logins"));
        builder.Entity<IdentityRoleClaim<Guid>>(e => e.ToTable("role_claims"));
        builder.Entity<IdentityUserRole<Guid>>(e => e.ToTable("user_roles"));

        new UserConfiguration().Configure(builder.Entity<User>());
        new OrganizationConfiguration().Configure(builder.Entity<Organization>());
        new CourseConfiguration().Configure(builder.Entity<Course>());
        new ChannelConfiguration().Configure(builder.Entity<Channel>());
        new AssignmentConfiguration().Configure(builder.Entity<Assignment>());
        new SubmissionConfiguration().Configure(builder.Entity<Submission>());
        new UserOrganizationConfiguration().Configure(builder.Entity<UserOrganization>());
        new OrganizationRoleConfiguration().Configure(builder.Entity<OrganizationRole>());
        new UserCourseConfiguration().Configure(builder.Entity<UserCourse>());
        new CourseRoleConfiguration().Configure(builder.Entity<CourseRole>());
        new ChannelParticipantConfiguration().Configure(builder.Entity<ChannelParticipant>());
        new ChannelRolePermissionConfiguration().Configure(builder.Entity<ChannelRolePermission>());
        new AttachmentConfiguration().Configure(builder.Entity<Attachment>());
        new CourseModuleConfiguration().Configure(builder.Entity<CourseModule>());
        new MessageConfiguration().Configure(builder.Entity<Message>());
    }
}