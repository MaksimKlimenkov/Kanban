using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kanban.Migrations
{
    /// <inheritdoc />
    public partial class FixUserReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_AspNetUsers_UserId1",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberId",
                table: "TaskExecutors");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId1",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId1",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OwnerId1",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_UserId1",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TaskExecutors_TeamMemberId",
                table: "TaskExecutors");

            migrationBuilder.DropIndex(
                name: "IX_TaskComments_UserId1",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TaskComments");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TaskComments",
                newName: "TeamMemberTeamId");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Teams",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "TeamMembers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "TeamMemberTeamId",
                table: "TaskExecutors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamMemberUserId",
                table: "TaskExecutors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TeamMemberId",
                table: "TaskComments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TeamMemberUserId",
                table: "TaskComments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                columns: new[] { "UserId", "TeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskExecutors",
                columns: new[] { "TeamMemberUserId", "TeamMemberTeamId" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskComments",
                columns: new[] { "TeamMemberUserId", "TeamMemberTeamId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_TeamMembers_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskComments",
                columns: new[] { "TeamMemberUserId", "TeamMemberTeamId" },
                principalTable: "TeamMembers",
                principalColumns: new[] { "UserId", "TeamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskExecutors",
                columns: new[] { "TeamMemberUserId", "TeamMemberTeamId" },
                principalTable: "TeamMembers",
                principalColumns: new[] { "UserId", "TeamId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId",
                table: "Teams",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_TeamMembers_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskExecutors");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_OwnerId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TaskExecutors_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskExecutors");

            migrationBuilder.DropIndex(
                name: "IX_TaskComments_TeamMemberUserId_TeamMemberTeamId",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "TeamMemberTeamId",
                table: "TaskExecutors");

            migrationBuilder.DropColumn(
                name: "TeamMemberUserId",
                table: "TaskExecutors");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "TeamMemberUserId",
                table: "TaskComments");

            migrationBuilder.RenameColumn(
                name: "TeamMemberTeamId",
                table: "TaskComments",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Teams",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "TeamMembers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TeamMembers",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TeamMembers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TaskComments",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamMembers",
                table: "TeamMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OwnerId1",
                table: "Teams",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_UserId1",
                table: "TeamMembers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaskExecutors_TeamMemberId",
                table: "TaskExecutors",
                column: "TeamMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_UserId1",
                table: "TaskComments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_AspNetUsers_UserId1",
                table: "TaskComments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskExecutors_TeamMembers_TeamMemberId",
                table: "TaskExecutors",
                column: "TeamMemberId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_AspNetUsers_UserId1",
                table: "TeamMembers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_AspNetUsers_OwnerId1",
                table: "Teams",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
