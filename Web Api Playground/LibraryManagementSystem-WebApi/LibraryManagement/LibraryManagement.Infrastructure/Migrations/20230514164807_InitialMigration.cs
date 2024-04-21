using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagement.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Isbn = table.Column<int>(type: "int", nullable: false),
                    AuthorName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BookEdition = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    StockAvailable = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    DeptId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__departme__BE2D26B6D0D236DC", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "designation",
                columns: table => new
                {
                    DesignationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DesignationName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_designation", x => x.DesignationId);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false),
                    StudentDepartment = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    DepartmentId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK__student__departm__3F466844",
                        column: x => x.DepartmentId,
                        principalTable: "department",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StaffName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DesignationId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK__staff__designati__70DDC3D8",
                        column: x => x.DesignationId,
                        principalTable: "designation",
                        principalColumn: "DesignationId");
                });

            migrationBuilder.CreateTable(
                name: "issue",
                columns: table => new
                {
                    IssueId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "date", nullable: false),
                    studentId = table.Column<int>(type: "int", nullable: true),
                    StaffId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_issue", x => x.IssueId);
                    table.ForeignKey(
                        name: "FK__issue__bookId__4222D4EF",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__issue__staffId__02084FDA",
                        column: x => x.StaffId,
                        principalTable: "Staff",
                        principalColumn: "StaffId");
                    table.ForeignKey(
                        name: "FK__issue__studentId__01142BA1",
                        column: x => x.studentId,
                        principalTable: "student",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "penalty",
                columns: table => new
                {
                    PenaltyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueId = table.Column<short>(type: "smallint", nullable: true),
                    PenaltyPaidStatus = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    PenaltyAmount = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penalty", x => x.PenaltyId);
                    table.ForeignKey(
                        name: "FK__penalty__issueId__04E4BC85",
                        column: x => x.IssueId,
                        principalTable: "issue",
                        principalColumn: "IssueId");
                });

            migrationBuilder.CreateTable(
                name: "return",
                columns: table => new
                {
                    ReturnId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpiryDate = table.Column<DateTime>(type: "date", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "date", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "date", nullable: false),
                    IssueId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_return", x => x.ReturnId);
                    table.ForeignKey(
                        name: "FK__return__bookId__38996AB5",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "BookId");
                    table.ForeignKey(
                        name: "FK__return__IssueId__4F47C5E3",
                        column: x => x.IssueId,
                        principalTable: "issue",
                        principalColumn: "IssueId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_issue_BookId",
                table: "issue",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_issue_StaffId",
                table: "issue",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_issue_studentId",
                table: "issue",
                column: "studentId");

            migrationBuilder.CreateIndex(
                name: "IX_penalty_IssueId",
                table: "penalty",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_return_BookId",
                table: "return",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_return_IssueId",
                table: "return",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_DesignationId",
                table: "Staff",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_student_DepartmentId",
                table: "student",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "penalty");

            migrationBuilder.DropTable(
                name: "return");

            migrationBuilder.DropTable(
                name: "issue");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "designation");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
