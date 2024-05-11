using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation_Project.Migrations
{
    /// <inheritdoc />
    public partial class EditToolPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToolImages1",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ToolImages2",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ToolImages3",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "ToolImages4",
                table: "Tools");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToolImages1",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToolImages2",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToolImages3",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToolImages4",
                table: "Tools",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
