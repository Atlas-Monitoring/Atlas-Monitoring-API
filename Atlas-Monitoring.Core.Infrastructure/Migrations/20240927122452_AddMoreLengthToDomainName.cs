using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreLengthToDomainName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Device",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldMaxLength: 15,
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 27, 14, 24, 50, 135, DateTimeKind.Local).AddTicks(3953),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 27, 14, 24, 50, 135, DateTimeKind.Local).AddTicks(3641),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3348));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Domain",
                table: "Device",
                type: "varchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3570),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 27, 14, 24, 50, 135, DateTimeKind.Local).AddTicks(3953));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3348),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 27, 14, 24, 50, 135, DateTimeKind.Local).AddTicks(3641));
        }
    }
}
