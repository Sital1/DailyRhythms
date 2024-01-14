using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyRhythms.Migrations
{
    /// <inheritdoc />
    public partial class Postgresdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DeletedAt",
                table: "ToDoItems",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2143), new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2146) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2148), new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2148) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2149), new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2149) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2150), new DateTime(2024, 1, 14, 2, 16, 46, 418, DateTimeKind.Utc).AddTicks(2150) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "ToDoItems",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3455), new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3457) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3458), new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3459), new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3460) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3460), new DateTime(2024, 1, 14, 1, 52, 41, 862, DateTimeKind.Utc).AddTicks(3461) });
        }
    }
}
