using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chalk.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganizationChannels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_messages_channel_participants_channel_id_user_id",
                table: "messages");

            migrationBuilder.DropTable(
                name: "channel_participants");

            migrationBuilder.RenameColumn(
                name: "profile_picture_uri",
                table: "users",
                newName: "profile_picture");

            migrationBuilder.RenameColumn(
                name: "profile_picture_uri",
                table: "organizations",
                newName: "profile_picture");

            migrationBuilder.AddColumn<long>(
                name: "organization_id",
                table: "channels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "organization_role_id",
                table: "channel_role_permissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "channel_users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    channel_id = table.Column<long>(type: "bigint", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    organization_role_id = table.Column<long>(type: "bigint", nullable: true),
                    course_role_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_users", x => new { x.user_id, x.channel_id });
                    table.ForeignKey(
                        name: "fk_channel_users_channel_role_permissions_channel_id_course_ro",
                        columns: x => new { x.channel_id, x.course_role_id },
                        principalTable: "channel_role_permissions",
                        principalColumns: new[] { "channel_id", "course_role_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_users_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_users_course_roles_course_role_id",
                        column: x => x.course_role_id,
                        principalTable: "course_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_users_organization_roles_organization_role_id",
                        column: x => x.organization_role_id,
                        principalTable: "organization_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_channel_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_channels_organization_id",
                table: "channels",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "ix_channel_role_permissions_organization_role_id",
                table: "channel_role_permissions",
                column: "organization_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_channel_id_course_role_id",
                table: "channel_users",
                columns: new[] { "channel_id", "course_role_id" });

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_course_role_id",
                table: "channel_users",
                column: "course_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_organization_role_id",
                table: "channel_users",
                column: "organization_role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_channel_role_permissions_organization_roles_organization_ro",
                table: "channel_role_permissions",
                column: "organization_role_id",
                principalTable: "organization_roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_channels_organizations_organization_id",
                table: "channels",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_channel_users_channel_id_user_id",
                table: "messages",
                columns: new[] { "channel_id", "user_id" },
                principalTable: "channel_users",
                principalColumns: new[] { "user_id", "channel_id" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_channel_role_permissions_organization_roles_organization_ro",
                table: "channel_role_permissions");

            migrationBuilder.DropForeignKey(
                name: "fk_channels_organizations_organization_id",
                table: "channels");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_channel_users_channel_id_user_id",
                table: "messages");

            migrationBuilder.DropTable(
                name: "channel_users");

            migrationBuilder.DropIndex(
                name: "ix_channels_organization_id",
                table: "channels");

            migrationBuilder.DropIndex(
                name: "ix_channel_role_permissions_organization_role_id",
                table: "channel_role_permissions");

            migrationBuilder.DropColumn(
                name: "organization_id",
                table: "channels");

            migrationBuilder.DropColumn(
                name: "organization_role_id",
                table: "channel_role_permissions");

            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "users",
                newName: "profile_picture_uri");

            migrationBuilder.RenameColumn(
                name: "profile_picture",
                table: "organizations",
                newName: "profile_picture_uri");

            migrationBuilder.CreateTable(
                name: "channel_participants",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    channel_id = table.Column<long>(type: "bigint", nullable: false),
                    course_role_id = table.Column<long>(type: "bigint", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_participants", x => new { x.user_id, x.channel_id });
                    table.ForeignKey(
                        name: "fk_channel_participants_channel_role_permissions_channel_id_co",
                        columns: x => new { x.channel_id, x.course_role_id },
                        principalTable: "channel_role_permissions",
                        principalColumns: new[] { "channel_id", "course_role_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_participants_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_participants_course_roles_course_role_id",
                        column: x => x.course_role_id,
                        principalTable: "course_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_participants_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_channel_participants_channel_id_course_role_id",
                table: "channel_participants",
                columns: new[] { "channel_id", "course_role_id" });

            migrationBuilder.CreateIndex(
                name: "ix_channel_participants_course_role_id",
                table: "channel_participants",
                column: "course_role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_channel_participants_channel_id_user_id",
                table: "messages",
                columns: new[] { "channel_id", "user_id" },
                principalTable: "channel_participants",
                principalColumns: new[] { "user_id", "channel_id" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
