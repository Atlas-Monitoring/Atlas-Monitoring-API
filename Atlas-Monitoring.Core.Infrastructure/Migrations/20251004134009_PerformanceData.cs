using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PerformanceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerData");

            migrationBuilder.DropTable(
                name: "ComputerHardDrive");

            migrationBuilder.CreateTable(
                name: "DeviceHardDrive",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Letter = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalSpace = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    SpaceUse = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHardDrive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceHardDrive_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DevicePerformanceData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProcessorUtilityPourcent = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    MemoryUsed = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    UptimeSinceInSecond = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePerformanceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DevicePerformanceData_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHardDrive_DeviceId",
                table: "DeviceHardDrive",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DevicePerformanceData_DeviceId",
                table: "DevicePerformanceData",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceHardDrive");

            migrationBuilder.DropTable(
                name: "DevicePerformanceData");

            migrationBuilder.CreateTable(
                name: "ComputerData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MemoryUsed = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    ProcessorUtilityPourcent = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    UptimeSinceInSecond = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerData_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComputerHardDrive",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Letter = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpaceUse = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    TotalSpace = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerHardDrive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerHardDrive_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerData_DeviceId",
                table: "ComputerData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerHardDrive_DeviceId",
                table: "ComputerHardDrive",
                column: "DeviceId");
        }
    }
}
