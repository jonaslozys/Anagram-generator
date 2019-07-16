using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    public partial class AddPrimaryKeyToUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLogs",
                table: "UserLogs");

            migrationBuilder.RenameTable(
                name: "UserLogs",
                newName: "SearchLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchLogs",
                table: "SearchLogs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIP = table.Column<string>(nullable: true),
                    AvailableSearches = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchLogs",
                table: "SearchLogs");

            migrationBuilder.RenameTable(
                name: "SearchLogs",
                newName: "UserLogs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLogs",
                table: "UserLogs",
                column: "Id");
        }
    }
}
