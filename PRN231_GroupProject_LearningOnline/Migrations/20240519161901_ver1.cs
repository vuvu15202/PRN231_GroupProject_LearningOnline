using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class ver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnroll_StudentFee_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.DropPrimaryKey(
                name: "PK__Grades__A8049041EBCA9414",
                table: "CourseEnroll");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnroll_StudentFeeId",
                table: "CourseEnroll");

            migrationBuilder.AddColumn<int>(
                name: "CourseEnrollId",
                table: "StudentFee",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentFeeId",
                table: "CourseEnroll",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseEnroll",
                table: "CourseEnroll",
                column: "CourseEnrollID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentFee_CourseEnrollId",
                table: "StudentFee",
                column: "CourseEnrollId",
                unique: true,
                filter: "[CourseEnrollId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnroll_CourseID",
                table: "CourseEnroll",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_CE_SF",
                table: "StudentFee",
                column: "CourseEnrollId",
                principalTable: "CourseEnroll",
                principalColumn: "CourseEnrollID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CE_SF",
                table: "StudentFee");

            migrationBuilder.DropIndex(
                name: "IX_StudentFee_CourseEnrollId",
                table: "StudentFee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseEnroll",
                table: "CourseEnroll");

            migrationBuilder.DropIndex(
                name: "IX_CourseEnroll_CourseID",
                table: "CourseEnroll");

            migrationBuilder.DropColumn(
                name: "CourseEnrollId",
                table: "StudentFee");

            migrationBuilder.AlterColumn<string>(
                name: "StudentFeeId",
                table: "CourseEnroll",
                type: "nvarchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK__Grades__A8049041EBCA9414",
                table: "CourseEnroll",
                columns: new[] { "CourseID", "UserID" });

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
                principalColumn: "StudentFeeId");
        }
    }
}
