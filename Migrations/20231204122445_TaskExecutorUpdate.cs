using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kanban.Migrations
{
    /// <inheritdoc />
    public partial class TaskExecutorUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskExecutors_AspNetUsers_UserId1",
                table: "TaskExecutors");

            migrationBuilder.DropIndex(
                name: "IX_TaskExecutors_UserId1",
                table: "TaskExecutors");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaskExecutors");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskExecutors",
                newName: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_TeamMemberId",
                table: "TaskExecutors",
                column: "TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberId",
                table: "TaskExecutors",
                column: "TeamMemberId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberId",
                table: "TaskExecutors");

            migrationBuilder.DropIndex(
                name: "IX_TaskExecutors_TeamMemberId",
                table: "TaskExecutors");

            migrationBuilder.RenameColumn(
                name: "TeamMemberId",
                table: "TaskExecutors",
                newName: "UserId");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaskExecutors",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_UserId1",
                table: "TaskExecutors",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskExecutors_AspNetUsers_UserId1",
                table: "TaskExecutors",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
