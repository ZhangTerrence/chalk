using chalk.Server.Common;

namespace chalk.Server.Utilities;

public static class OrganizationPermissionsUtilities
{
    public static long OwnerPermissions()
    {
        return -1;
    }

    public static long BaseAdminPermissions()
    {
        return (long)OrganizationPermission.ViewUsers
               | (long)OrganizationPermission.ManageUsers
               | (long)OrganizationPermission.InviteUsers
               | (long)OrganizationPermission.BanUsers
               | (long)OrganizationPermission.ViewCourses
               | (long)OrganizationPermission.ManageCourses
               | (long)OrganizationPermission.ViewRoles
               | (long)OrganizationPermission.ManageRoles;
    }

    public static long BaseUserPermissions()
    {
        return (long)OrganizationPermission.ViewUsers
               | (long)OrganizationPermission.InviteUsers
               | (long)OrganizationPermission.ViewCourses;
    }

    public static long GeneratePermissions(params OrganizationPermission[] permissions)
    {
        return permissions.Aggregate(0L, (x, y) => x | (long)y);
    }
}