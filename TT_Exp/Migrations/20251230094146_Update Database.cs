using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TT_Exp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingStatus",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "HoldExpiresAt",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Slots");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "BookingStatus",
                table: "Slots",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "HoldExpiresAt",
                table: "Slots",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Slots",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
