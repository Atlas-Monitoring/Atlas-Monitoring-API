using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDateAddOfComputerHardDrive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "ComputerHardDrive");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6303),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7721));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6074),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7484));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7721),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7484),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6074));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "ComputerHardDrive",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
