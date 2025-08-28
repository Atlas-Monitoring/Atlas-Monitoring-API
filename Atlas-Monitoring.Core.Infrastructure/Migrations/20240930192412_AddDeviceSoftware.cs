using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceSoftware : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceSoftwareInstalled",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AppName = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Version = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Publisher = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_DeviceSoftwareInstalled_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceSoftwareInstalled_DeviceId",
                table: "DeviceSoftwareInstalled",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceSoftwareInstalled");
        }
    }
}
