using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Atlas_Monitoring.Core.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LimitLengthOfModelAndManufacturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Device",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Device",
                type: "varchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateUpdated",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 28, 23, 56, 23, 805, DateTimeKind.Local).AddTicks(4356),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 28, 23, 56, 23, 805, DateTimeKind.Local).AddTicks(4124),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3348));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Device",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Manufacturer",
                table: "Device",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(60)",
                oldMaxLength: 60)
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
                oldDefaultValue: new DateTime(2024, 9, 28, 23, 56, 23, 805, DateTimeKind.Local).AddTicks(4356));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdd",
                table: "Device",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 9, 14, 12, 39, 34, 924, DateTimeKind.Local).AddTicks(3348),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 9, 28, 23, 56, 23, 805, DateTimeKind.Local).AddTicks(4124));
        }
    }
}
