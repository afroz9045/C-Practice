using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnrollmentLib.Migrations
{
    public partial class CompositeKeyAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseID",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "Enrollment",
                table: "Courses");

            migrationBuilder.AddColumn<int>(
                name: "CourseCredits",
                schema: "Enrollment",
                table: "Enrollments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                schema: "Enrollment",
                table: "Enrollments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Credits",
                schema: "Enrollment",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                schema: "Enrollment",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)")
                .OldAnnotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                schema: "Enrollment",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("Relational:ColumnOrder", 2)
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "Enrollment",
                table: "Courses",
                columns: new[] { "Credits", "CourseName" });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseCredits_CourseTitle",
                schema: "Enrollment",
                table: "Enrollments",
                columns: new[] { "CourseCredits", "CourseTitle" });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseCredits_CourseTitle",
                schema: "Enrollment",
                table: "Enrollments",
                columns: new[] { "CourseCredits", "CourseTitle" },
                principalSchema: "Enrollment",
                principalTable: "Courses",
                principalColumns: new[] { "Credits", "CourseName" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseCredits_CourseTitle",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_CourseCredits_CourseTitle",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                schema: "Enrollment",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseCredits",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "CourseTitle",
                schema: "Enrollment",
                table: "Enrollments");

            migrationBuilder.AlterColumn<int>(
                name: "CourseID",
                schema: "Enrollment",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 2)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "CourseName",
                schema: "Enrollment",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("Relational:ColumnOrder", 0);

            migrationBuilder.AlterColumn<int>(
                name: "Credits",
                schema: "Enrollment",
                table: "Courses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                schema: "Enrollment",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseID",
                schema: "Enrollment",
                table: "Enrollments",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                schema: "Enrollment",
                table: "Enrollments",
                column: "CourseID",
                principalSchema: "Enrollment",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
