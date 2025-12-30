using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TT_Exp.Migrations
{
    /// <inheritdoc />
    public partial class addingTabledescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TableDescription",
                table: "Tables",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableDescription",
                table: "Tables");
        }
    }
}
