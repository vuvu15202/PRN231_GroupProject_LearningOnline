using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification",
                column: "NotificationTo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification",
                column: "NotificationTo",
                unique: true);
        }
    }
}
