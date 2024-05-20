using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CourseEnroll_CourseID",
                table: "CourseEnroll");

            migrationBuilder.DropColumn(
                name: "PublishStatus",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Request",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "StudentFeeId",
                table: "CourseEnroll",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "Price",
                table: "Course",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Grades__A8049041EBCA9414",
                table: "CourseEnroll",
                columns: new[] { "CourseID", "UserID" });

            migrationBuilder.CreateTable(
                name: "StudentFee",
                columns: table => new
                {
                    StudentFeeId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BankCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Amount = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OrderInfo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ErrorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LocalMessage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfPaid = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentFee", x => x.StudentFeeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnroll_StudentFeeId",
                table: "CourseEnroll",
                column: "StudentFeeId",
                unique: true,
                filter: "[StudentFeeId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll",
                column: "StudentFeeId",
                principalTable: "StudentFee",
                principalColumn: "StudentFeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.DropTable(
                name: "StudentFee");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Grades__A8049041EBCA9414",
                table: "CourseEnroll");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnroll_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.DropColumn(
                name: "StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Course");

            migrationBuilder.AddColumn<int>(
                name: "PublishStatus",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Request",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnroll_CourseID",
                table: "CourseEnroll",
                column: "CourseID");
        }
    }
}
