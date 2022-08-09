using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class SchemaUpdateForModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Students",
                newName: "Students",
                newSchema: "Enrollment");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                newName: "Enrollments",
                newSchema: "Enrollment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Students",
                schema: "Enrollment",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Enrollments",
                schema: "Enrollment",
                newName: "Enrollments");
        }
    }
}
