using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class SchemaUpdateForAll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Course",
                newName: "Courses",
                newSchema: "Enrollment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Course");

            migrationBuilder.RenameTable(
                name: "Courses",
                schema: "Enrollment",
                newName: "Courses",
                newSchema: "Course");
        }
    }
}
