using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chalk.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedExistingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attachments_assignment_assignment_id",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_channel_users_channel_id_user_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_submissions_assignment_assignment_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "channel_users");

            migrationBuilder.DropIndex(
                name: "ix_messages_channel_id_user_id",
                table: "messages");

            migrationBuilder.AlterColumn<int>(
                name: "grade",
                table: "user_courses",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<bool>(
                name: "student",
                table: "user_courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "user_channels",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    channel_id = table.Column<long>(type: "bigint", nullable: false),
                    joined_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    course_role_id = table.Column<long>(type: "bigint", nullable: false),
                    organization_role_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_channels", x => new { x.user_id, x.channel_id });
                    table.ForeignKey(
                        name: "fk_user_channels_channel_role_permissions_channel_id_course_ro",
                        columns: x => new { x.channel_id, x.course_role_id },
                        principalTable: "channel_role_permissions",
                        principalColumns: new[] { "channel_id", "course_role_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_channels_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_channels_course_roles_course_role_id",
                        column: x => x.course_role_id,
                        principalTable: "course_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_channels_organization_roles_organization_role_id",
                        column: x => x.organization_role_id,
                        principalTable: "organization_roles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_channels_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_messages_channel_id",
                table: "messages",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_user_id_channel_id",
                table: "messages",
                columns: new[] { "user_id", "channel_id" });

            migrationBuilder.CreateIndex(
                name: "ix_user_channels_channel_id_course_role_id",
                table: "user_channels",
                columns: new[] { "channel_id", "course_role_id" });

            migrationBuilder.CreateIndex(
                name: "ix_user_channels_course_role_id",
                table: "user_channels",
                column: "course_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_channels_organization_role_id",
                table: "user_channels",
                column: "organization_role_id");

            migrationBuilder.AddForeignKey(
                name: "fk_attachments_assignments_assignment_id",
                table: "attachments",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_user_channels_user_id_channel_id",
                table: "messages",
                columns: new[] { "user_id", "channel_id" },
                principalTable: "user_channels",
                principalColumns: new[] { "user_id", "channel_id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_submissions_assignments_assignment_id",
                table: "submissions",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_attachments_assignments_assignment_id",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_messages_user_channels_user_id_channel_id",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "fk_submissions_assignments_assignment_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "user_channels");

            migrationBuilder.DropIndex(
                name: "ix_messages_channel_id",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "ix_messages_user_id_channel_id",
                table: "messages");

            migrationBuilder.DropColumn(
                name: "student",
                table: "user_courses");

            migrationBuilder.AlterColumn<int>(
                name: "grade",
                table: "user_courses",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "channel_users",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    channel_id = table.Column<long>(type: "bigint", nullable: false),
                    course_role_id = table.Column<long>(type: "bigint", nullable: false),
                    organization_role_id = table.Column<long>(type: "bigint", nullable: true),
                    joined_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "ix_messages_channel_id_user_id",
                table: "messages",
                columns: new[] { "channel_id", "user_id" });

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
                name: "fk_attachments_assignment_assignment_id",
                table: "attachments",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_messages_channel_users_channel_id_user_id",
                table: "messages",
                columns: new[] { "channel_id", "user_id" },
                principalTable: "channel_users",
                principalColumns: new[] { "user_id", "channel_id" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_submissions_assignment_assignment_id",
                table: "submissions",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
