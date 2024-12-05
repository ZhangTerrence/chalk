using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chalk.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 577, DateTimeKind.Utc).AddTicks(9841),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(3290));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 577, DateTimeKind.Utc).AddTicks(9492),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(2888));

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "users",
                type: "character varying(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "full_name",
                table: "users",
                type: "character varying(63)",
                maxLength: 63,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "users",
                type: "character varying(31)",
                maxLength: 31,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "profile_picture",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_date",
                table: "user_organizations",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(2622));

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "user_organizations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_date",
                table: "user_courses",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(8658));

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "user_courses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "submissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(2825),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(8813));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "submissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(2429),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(8476));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(4034),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(9100));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(3733),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(8735));

            migrationBuilder.AddColumn<string>(
                name: "profile_picture",
                table: "organizations",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "organization_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(8952),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(5784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "organization_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(8736),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(5427));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(4587),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(3218));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(4297),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(2907));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(8470),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(3616));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(8139),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(3294));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "course_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 581, DateTimeKind.Utc).AddTicks(4280),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 128, DateTimeKind.Utc).AddTicks(1723));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "course_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 581, DateTimeKind.Utc).AddTicks(4067),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 128, DateTimeKind.Utc).AddTicks(1494));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "course_modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(3233),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(1772));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "course_modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(2932),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(1396));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "channels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(4158),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(172));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "channels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(3823),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(9801));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "attachments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(418),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 130, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "attachments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(27),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 130, DateTimeKind.Utc).AddTicks(8619));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(9358),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(5378));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(9113),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(5057));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "full_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profile_picture",
                table: "users");

            migrationBuilder.DropColumn(
                name: "status",
                table: "user_organizations");

            migrationBuilder.DropColumn(
                name: "status",
                table: "user_courses");

            migrationBuilder.DropColumn(
                name: "profile_picture",
                table: "organizations");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(3290),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 577, DateTimeKind.Utc).AddTicks(9841));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(2888),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 577, DateTimeKind.Utc).AddTicks(9492));

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_date",
                table: "user_organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(2622),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "joined_date",
                table: "user_courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(8658),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "submissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(8813),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(2825));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "submissions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(8476),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(2429));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(9100),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(4034));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "organizations",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 124, DateTimeKind.Utc).AddTicks(8735),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(3733));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "organization_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(5784),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(8952));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "organization_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 127, DateTimeKind.Utc).AddTicks(5427),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 580, DateTimeKind.Utc).AddTicks(8736));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(3218),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(4587));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(2907),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(4297));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(3616),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(8470));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "courses",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(3294),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 578, DateTimeKind.Utc).AddTicks(8139));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "course_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 128, DateTimeKind.Utc).AddTicks(1723),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 581, DateTimeKind.Utc).AddTicks(4280));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "course_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 128, DateTimeKind.Utc).AddTicks(1494),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 581, DateTimeKind.Utc).AddTicks(4067));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "course_modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(1772),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(3233));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "course_modules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 131, DateTimeKind.Utc).AddTicks(1396),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(2932));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "channels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(172),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(4158));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "channels",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 125, DateTimeKind.Utc).AddTicks(9801),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(3823));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "attachments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 130, DateTimeKind.Utc).AddTicks(8970),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(418));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "attachments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 130, DateTimeKind.Utc).AddTicks(8619),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 584, DateTimeKind.Utc).AddTicks(27));

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_date",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(5378),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(9358));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_date",
                table: "assignments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(2024, 11, 26, 17, 23, 12, 126, DateTimeKind.Utc).AddTicks(5057),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValue: new DateTime(2024, 12, 5, 0, 1, 59, 579, DateTimeKind.Utc).AddTicks(9113));
        }
    }
}
