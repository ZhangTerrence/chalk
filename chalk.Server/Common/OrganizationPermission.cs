namespace chalk.Server.Common;

public enum OrganizationPermission : long
{
    ViewUsers = 1 << 0,
    ManageUsers = 1 << 1,
    InviteUsers = 1 << 2,
    KickUsers = 1 << 3,
    BanUsers = 1 << 4,
    ViewCourses = 1 << 5,
    ManageCourses = 1 << 6,
    ViewRoles = 1 << 7,
    ManageRoles = 1 << 8,
    DeleteOrganization = 1 << 9
}