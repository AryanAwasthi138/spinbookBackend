using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TT_Exp.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_AspNetUsers_UserName",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_UserName",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Slots");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Slots",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Slots_Id",
                table: "Slots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_AspNetUsers_Id",
                table: "Slots",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_AspNetUsers_Id",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_Id",
                table: "Slots");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Slots");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Slots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_UserName",
                table: "Slots",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_AspNetUsers_UserName",
                table: "Slots",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
