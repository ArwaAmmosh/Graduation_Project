using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation_Project.Migrations
{
    /// <inheritdoc />
    public partial class EditUniversity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Acadmicyear",
                table: "Tools",
                newName: "AcademicYear");

            migrationBuilder.RenameColumn(
                name: "Univserity",
                table: "AspNetUsers",
                newName: "University");

            migrationBuilder.RenameColumn(
                name: "AcadmicYear",
                table: "AspNetUsers",
                newName: "AcademicYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AcademicYear",
                table: "Tools",
                newName: "Acadmicyear");

            migrationBuilder.RenameColumn(
                name: "University",
                table: "AspNetUsers",
                newName: "Univserity");

            migrationBuilder.RenameColumn(
                name: "AcademicYear",
                table: "AspNetUsers",
                newName: "AcadmicYear");
        }
    }
}
