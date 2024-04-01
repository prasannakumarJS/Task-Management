using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "RoleType");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(8574),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8374));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(8335),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8129));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9255),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8970));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9025),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8798));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d06802a2-8193-4e92-86f7-f8dae5109306"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9484), new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9485) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleType",
                table: "Users",
                newName: "Role");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8374),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(8574));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8129),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(8335));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8970),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9255));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(8798),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 3, 31, 11, 57, 21, 220, DateTimeKind.Utc).AddTicks(9025));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d06802a2-8193-4e92-86f7-f8dae5109306"),
                columns: new[] { "Created", "LastUpdated" },
                values: new object[] { new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(9132), new DateTime(2024, 3, 31, 11, 54, 47, 242, DateTimeKind.Utc).AddTicks(9136) });
        }
    }
}
