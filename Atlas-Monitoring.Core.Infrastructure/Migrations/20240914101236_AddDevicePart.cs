using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDevicePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 14, 12, 12, 35, 554, DateTimeKind.Local).AddTicks(9462),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(5235));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 14, 12, 12, 35, 554, DateTimeKind.Local).AddTicks(9153),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(4998));

            migrationBuilder.CreateTable(
                name: "DeviceParts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Labels = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceParts_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceParts_DeviceId",
                table: "DeviceParts",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceParts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(5235),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 12, 35, 554, DateTimeKind.Local).AddTicks(9462));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 7, 10, 2, 5, 34, 468, DateTimeKind.Local).AddTicks(4998),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 12, 35, 554, DateTimeKind.Local).AddTicks(9153));
        }
    }
}
