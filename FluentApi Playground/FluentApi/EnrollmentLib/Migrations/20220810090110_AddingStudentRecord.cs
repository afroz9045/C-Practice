using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class AddingStudentRecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Enrollment",
                table: "Students",
                columns: new[] { "ID", "EnrollmentDate", "FirstName", "LastName" },
                values: new object[] { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sarfaraz", "khan" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Enrollment",
                table: "Students",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
