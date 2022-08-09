using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class CourseSchemaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Course");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Courses",
                newSchema: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Course",
                newName: "Courses");
        }
    }
}
