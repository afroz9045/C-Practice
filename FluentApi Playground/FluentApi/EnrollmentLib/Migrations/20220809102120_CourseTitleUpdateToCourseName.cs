using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class CourseTitleUpdateToCourseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Courses",
                newName: "CourseName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Courses",
                newName: "Title");
        }
    }
}
