using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_UserID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_UserID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CourseInfo",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Course");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Category");

            migrationBuilder.AddColumn<string>(
                name: "CourseInfo",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_UserID",
                table: "Course",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_UserID",
                table: "Course",
                column: "UserID",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
