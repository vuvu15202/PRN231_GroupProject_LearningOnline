using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class versin2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviousLessionId",
                table: "Lesson",
                type: "int",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Lesson_PreviousLessionId",
                table: "Lesson");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_PreviousLessionId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "PreviousLessionId",
                table: "Lesson");
        }
    }
}
