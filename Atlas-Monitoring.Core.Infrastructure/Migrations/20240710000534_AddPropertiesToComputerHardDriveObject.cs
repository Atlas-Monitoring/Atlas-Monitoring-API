using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertiesToComputerHardDriveObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerData_Device_DeviceId",
                table: "ComputerData");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerHardDrive_Device_DeviceId",
                table: "ComputerHardDrive");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceHistory_Device_DeviceId",
                table: "DeviceHistory");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(5235),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6303));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(4998),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6074));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdd",
                table: "ComputerHardDrive",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdate",
                table: "ComputerHardDrive",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerData_Device_DeviceId",
                table: "ComputerData",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerHardDrive_Device_DeviceId",
                table: "ComputerHardDrive",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device",
                column: "DeviceTypeId",
                principalTable: "DeviceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHistory_Device_DeviceId",
                table: "DeviceHistory",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComputerData_Device_DeviceId",
                table: "ComputerData");

            migrationBuilder.DropForeignKey(
                name: "FK_ComputerHardDrive_Device_DeviceId",
                table: "ComputerHardDrive");

            migrationBuilder.DropForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceHistory_Device_DeviceId",
                table: "DeviceHistory");

            migrationBuilder.DropColumn(
                name: "DateAdd",
                table: "ComputerHardDrive");

            migrationBuilder.DropColumn(
                name: "DateUpdate",
                table: "ComputerHardDrive");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6303),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 5, 22, 42, 41, 10, DateTimeKind.Local).AddTicks(6074),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerData_Device_DeviceId",
                table: "ComputerData",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComputerHardDrive_Device_DeviceId",
                table: "ComputerHardDrive",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_DeviceType_DeviceTypeId",
                table: "Device",
                column: "DeviceTypeId",
                principalTable: "DeviceType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceHistory_Device_DeviceId",
                table: "DeviceHistory",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
