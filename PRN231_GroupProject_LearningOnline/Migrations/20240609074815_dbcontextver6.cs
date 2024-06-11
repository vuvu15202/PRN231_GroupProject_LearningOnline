using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Lesson_PreviousLessionId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_PreviousLessionId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "PreviousLessionId",
                table: "Lesson",
                newName: "PreviousLessioNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreviousLessioNum",
                table: "Lesson",
                newName: "PreviousLessionId");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lesson",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_PreviousLessionId",
                table: "Lesson",
                column: "PreviousLessionId",
                unique: true,
                filter: "[PreviousLessionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Lesson_PreviousLessionId",
                table: "Lesson",
                column: "PreviousLessionId",
                principalTable: "Lesson",
                principalColumn: "LessonID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
