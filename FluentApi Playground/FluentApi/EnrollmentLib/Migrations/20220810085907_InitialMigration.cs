using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Enrollment");

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "Enrollment",
                columns: table => new
                {
                    CourseName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Credits = table.Column<int>(type: "int", nullable: false),
                    CourseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => new { x.Credits, x.CourseName });
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "Enrollment",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                schema: "Enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<int>(type: "int", nullable: false),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    CourseCredits = table.Column<int>(type: "int", nullable: false),
                    CourseTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseCredits_CourseTitle",
                        columns: x => new { x.CourseCredits, x.CourseTitle },
                        principalSchema: "Enrollment",
                        principalTable: "Courses",
                        principalColumns: new[] { "Credits", "CourseName" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentID",
                        column: x => x.StudentID,
                        principalSchema: "Enrollment",
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Enrollment",
                table: "Students",
                columns: new[] { "ID", "EnrollmentDate", "FirstName", "LastName" },
                values: new object[] { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shabaz", "khan" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseCredits_CourseTitle",
                schema: "Enrollment",
                table: "Enrollments",
                columns: new[] { "CourseCredits", "CourseTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentID",
                schema: "Enrollment",
                table: "Enrollments",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments",
                schema: "Enrollment");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "Enrollment");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "Enrollment");
        }
    }
}
