using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Path_Green.web.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIDAndSchoolToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentID",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "Orders");
        }
    }
}
