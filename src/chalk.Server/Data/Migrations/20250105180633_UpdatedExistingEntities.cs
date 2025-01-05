using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace chalk.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExistingEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_assignments_courses_course_id",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_attachments_assignments_assignment_id",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_courses_organizations_organization_id",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "fk_submissions_assignments_assignment_id",
                table: "submissions");

            migrationBuilder.DropColumn(
                name: "max_grade",
                table: "assignments");

            migrationBuilder.RenameColumn(
                name: "uri",
                table: "attachments",
                newName: "resource");

            migrationBuilder.RenameColumn(
                name: "course_id",
                table: "assignments",
                newName: "assignment_group_id");

            migrationBuilder.RenameIndex(
                name: "ix_assignments_course_id",
                table: "assignments",
                newName: "ix_assignments_assignment_group_id");

            migrationBuilder.AddColumn<int>(
                name: "grade",
                table: "user_courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "rank",
                table: "organization_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "organization_id",
                table: "courses",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "preview_image",
                table: "courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "public",
                table: "courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "rank",
                table: "course_roles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "order",
                table: "course_modules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "attachments",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "assignment_groups",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    weight = table.Column<int>(type: "integer", nullable: false),
                    course_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_assignment_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_assignment_groups_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(31)", maxLength: 31, nullable: false),
                    course_id = table.Column<long>(type: "bigint", nullable: true),
                    organization_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tags", x => x.id);
                    table.ForeignKey(
                        name: "fk_tags_courses_course_id",
                        column: x => x.course_id,
                        principalTable: "courses",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_tags_organizations_organization_id",
                        column: x => x.organization_id,
                        principalTable: "organizations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_assignment_groups_course_id",
                table: "assignment_groups",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_course_id",
                table: "tags",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "ix_tags_organization_id",
                table: "tags",
                column: "organization_id");

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_assignment_groups_assignment_group_id",
                table: "assignments",
                column: "assignment_group_id",
                principalTable: "assignment_groups",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_attachments_assignment_assignment_id",
                table: "attachments",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_courses_organizations_organization_id",
                table: "courses",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_submissions_assignment_assignment_id",
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
                name: "fk_assignments_assignment_groups_assignment_group_id",
                table: "assignments");

            migrationBuilder.DropForeignKey(
                name: "fk_attachments_assignment_assignment_id",
                table: "attachments");

            migrationBuilder.DropForeignKey(
                name: "fk_courses_organizations_organization_id",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "fk_submissions_assignment_assignment_id",
                table: "submissions");

            migrationBuilder.DropTable(
                name: "assignment_groups");

            migrationBuilder.DropTable(
                name: "tags");

            migrationBuilder.DropColumn(
                name: "grade",
                table: "user_courses");

            migrationBuilder.DropColumn(
                name: "rank",
                table: "organization_roles");

            migrationBuilder.DropColumn(
                name: "preview_image",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "public",
                table: "courses");

            migrationBuilder.DropColumn(
                name: "rank",
                table: "course_roles");

            migrationBuilder.DropColumn(
                name: "order",
                table: "course_modules");

            migrationBuilder.DropColumn(
                name: "description",
                table: "attachments");

            migrationBuilder.RenameColumn(
                name: "resource",
                table: "attachments",
                newName: "uri");

            migrationBuilder.RenameColumn(
                name: "assignment_group_id",
                table: "assignments",
                newName: "course_id");

            migrationBuilder.RenameIndex(
                name: "ix_assignments_assignment_group_id",
                table: "assignments",
                newName: "ix_assignments_course_id");

            migrationBuilder.AlterColumn<long>(
                name: "organization_id",
                table: "courses",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "max_grade",
                table: "assignments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_assignments_courses_course_id",
                table: "assignments",
                column: "course_id",
                principalTable: "courses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_attachments_assignments_assignment_id",
                table: "attachments",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_courses_organizations_organization_id",
                table: "courses",
                column: "organization_id",
                principalTable: "organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_submissions_assignments_assignment_id",
                table: "submissions",
                column: "assignment_id",
                principalTable: "assignments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
