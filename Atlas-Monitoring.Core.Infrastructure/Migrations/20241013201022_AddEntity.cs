using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntityId",
                table: "Device",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    EntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false, defaultValue: "")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.EntityId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Device_EntityId",
                table: "Device",
                column: "EntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Entity_EntityId",
                table: "Device",
                column: "EntityId",
                principalTable: "Entity",
                principalColumn: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Entity_EntityId",
                table: "Device");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropIndex(
                name: "IX_Device_EntityId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "EntityId",
                table: "Device");
        }
    }
}
