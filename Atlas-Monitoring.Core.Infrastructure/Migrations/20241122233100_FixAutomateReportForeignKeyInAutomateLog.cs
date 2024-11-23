using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAutomateReportForeignKeyInAutomateLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AutomateId",
                table: "AutomateLog",
                newName: "AutomateReportId");

            migrationBuilder.CreateIndex(
                name: "IX_AutomateLog_AutomateReportId",
                table: "AutomateLog",
                column: "AutomateReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomateLog_AutomateReport_AutomateReportId",
                table: "AutomateLog",
                column: "AutomateReportId",
                principalTable: "AutomateReport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomateLog_AutomateReport_AutomateReportId",
                table: "AutomateLog");

            migrationBuilder.DropIndex(
                name: "IX_AutomateLog_AutomateReportId",
                table: "AutomateLog");

            migrationBuilder.RenameColumn(
                name: "AutomateReportId",
                table: "AutomateLog",
                newName: "AutomateId");
        }
    }
}
