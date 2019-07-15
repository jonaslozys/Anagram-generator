using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    public partial class RemoveUserLogToWordReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Words_WordSearchedId",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_WordSearchedId",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "WordSearchedId",
                table: "UserLogs");

            migrationBuilder.AddColumn<string>(
                name: "WordSearched",
                table: "UserLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordSearched",
                table: "UserLogs");

            migrationBuilder.AddColumn<int>(
                name: "WordSearchedId",
                table: "UserLogs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_WordSearchedId",
                table: "UserLogs",
                column: "WordSearchedId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Words_WordSearchedId",
                table: "UserLogs",
                column: "WordSearchedId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
