using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN231_GroupProject_LearningOnline.Migrations
{
    public partial class dbcontextver9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notification",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification",
                column: "NotificationTo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User",
                table: "Notification",
                column: "NotificationTo",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_NotificationTo",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notification");
        }
    }
}
