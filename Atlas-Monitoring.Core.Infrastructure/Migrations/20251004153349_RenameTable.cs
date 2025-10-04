using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("ComputerData", newName: "DevicePerformanceData");
            migrationBuilder.RenameTable("ComputerHardDrive", newName: "DeviceHardDrive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("DevicePerformanceData", newName: "ComputerData");
            migrationBuilder.RenameTable("DeviceHardDrive", newName: "ComputerHardDrive");
        }
    }
}
