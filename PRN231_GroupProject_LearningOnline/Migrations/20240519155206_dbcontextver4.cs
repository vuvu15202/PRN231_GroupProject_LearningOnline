using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId1",
                table: "CourseEnroll");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnroll_StudentFeeId1",
                table: "CourseEnroll");

            migrationBuilder.DropColumn(
                name: "StudentFeeId1",
                table: "CourseEnroll");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll",
                column: "StudentFeeId",
                principalTable: "StudentFee",
                principalColumn: "StudentFeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.AddColumn<string>(
                name: "StudentFeeId1",
                table: "CourseEnroll",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnroll_StudentFeeId1",
                table: "CourseEnroll",
                column: "StudentFeeId1",
                unique: true,
                filter: "[StudentFeeId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll",
                column: "StudentFeeId",
                principalTable: "StudentFee",
                principalColumn: "StudentFeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId1",
                table: "CourseEnroll",
                column: "StudentFeeId1",
                principalTable: "StudentFee",
                principalColumn: "StudentFeeId");
        }
    }
}
