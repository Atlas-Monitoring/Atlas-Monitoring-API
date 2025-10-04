using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceStatus = table.Column<int>(type: "int", nullable: false),
                    DeviceTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ip = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domain = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaxRam = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    NumberOfLogicalProcessors = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    OS = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OSVersion = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(48)", maxLength: 48, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SerialNumber = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7484)),
                    DateUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(2024, 7, 5, 1, 48, 40, 710, DateTimeKind.Local).AddTicks(7721))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_DeviceType_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComputerData",
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
                    table.PrimaryKey("PK_ComputerData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerData_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComputerHardDrive",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Letter = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalSpace = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0),
                    SpaceUse = table.Column<double>(type: "double", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComputerHardDrive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComputerHardDrive_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeviceHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DeviceId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DateAdd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Message = table.Column<string>(type: "varchar(240)", maxLength: 240, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceHistory_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "DeviceType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Undefined" },
                    { 2, "Computer" },
                    { 3, "Server" },
                    { 4, "Printer" },
                    { 5, "Router" },
                    { 6, "Switch" },
                    { 7, "Phone" },
                    { 8, "MobilePhone" },
                    { 9, "Other" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComputerData_DeviceId",
                table: "ComputerData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerHardDrive_DeviceId",
                table: "ComputerHardDrive",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceTypeId",
                table: "Device",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistory_DeviceId",
                table: "DeviceHistory",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComputerData");

            migrationBuilder.DropTable(
                name: "ComputerHardDrive");

            migrationBuilder.DropTable(
                name: "DeviceHistory");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "DeviceType");
        }
    }
}
