using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.Ef.CodeFirst.Migrations
{
    public partial class AddDefaultAvailableSearchesValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSearches",
                table: "Users",
                nullable: false,
                defaultValue: 20,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AvailableSearches",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 20);
        }
    }
}
